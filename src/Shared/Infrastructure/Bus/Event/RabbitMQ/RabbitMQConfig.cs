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
                Port = configParams.Port
            };
            exchange = configParams.Exchange;
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
                _channel.ExchangeDeclare(exchange.Name, ExchangeType.Topic);
                foreach (Subscribers subscriber in exchange.Subscribers)
                {
                    var queue = _channel.QueueDeclare(subscriber.QueuName,
                    true,
                    false,
                    false);
                    _channel.QueueBind(queue, exchange.Name, subscriber.EventName);
                }
            }
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
