using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Shared.Infrastructure.Bus.Event.RabbitMQ;

// Base on C#-ddd-scheleton by CodelyTV: https://github.com/CodelyTV/csharp-ddd-skeleton
namespace Clouds.LastBackups.Infraestructure.Bus.RabbitMQ
{
    public class RabbitMQConfig : IDisposable
    {
        private ConnectionFactory ConnectionFactory { get; }
        private static IConnection _connection { get; set; }
        private static IModel _channel { get; set; }
        private Exchanges exchange { get; }
        private int deliveryLimit { get; }

        public string ExchangeName
        {
            get
            {
                return (null != exchange) ? exchange.Name : string.Empty;
            }
        }

        public RabbitMQConfig(IOptions<RabbitMQSettings> rabbitMqParams)
        {
            RabbitMQSettings configParams = rabbitMqParams.Value;

            ConnectionFactory = new ConnectionFactory
            {
                HostName = configParams.HostName,
                UserName = configParams.UserName,
                Password = configParams.Password,
                Port = configParams.Port,
                AutomaticRecoveryEnabled = true
            };
            exchange = configParams.Exchange;
            deliveryLimit = configParams.DeliveryLimit;
        }

        public IConnection Connection()
        {
            if (_connection == null) _connection = ConnectionFactory.CreateConnection();

            return _connection;
        }

        public IModel Channel()
        {
            if (_channel == null)
            {
                _channel = Connection().CreateModel();
                configExchange();
            }
            return _channel;
        }

        private void configExchange()
        {
            if (null != exchange)
            {
                string deadLetterExchangeName = RabbitMqExchangeNameFormatter.DeadLetter(exchange.Name);

                _channel.ExchangeDeclare(exchange.Name, ExchangeType.Topic);
                _channel.ExchangeDeclare(deadLetterExchangeName, ExchangeType.Topic);

                foreach (Subscribers subscriber in exchange.Subscribers)
                {
                    Dictionary<String, Object> args = new()
                    {
                        {"x-dead-letter-exchange", deadLetterExchangeName},
                        {"x-queue-type", "quorum"},
                        {"x-delivery-limit", deliveryLimit}
                    };
                    var queue = _channel.QueueDeclare(subscriber.QueueName,
                    true,
                    false,
                    false,
                    args);

                    string deadLetterQueueName = RabbitMQQueueNameFormatter.DeadLetter(subscriber.QueueName);
                    var deadLetterQueue = _channel.QueueDeclare(deadLetterQueueName, true,
                        false,
                        false);

                    _channel.QueueBind(queue, exchange.Name, subscriber.EventName);
                    _channel.QueueBind(deadLetterQueue, deadLetterExchangeName, subscriber.EventName);
                }
            }
        }
        private IDictionary<string, object> RetryQueueArguments(string domainEventExchange,
            string domainEventQueue)
        {
            return new Dictionary<string, object>
            {
                {"x-dead-letter-exchange", domainEventExchange},
                {"x-dead-letter-routing-key", domainEventQueue},
                {"x-message-ttl", 1000}
            };
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
