using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Shared.Domain.Bus.Event;
using Shared.Infrastructure.Bus.Event.RabbitMQ;

// Base on C#-ddd-scheleton by CodelyTV: https://github.com/CodelyTV/csharp-ddd-skeleton
namespace Clouds.LastBackups.Infraestructure.Bus.RabbitMQ
{
    public class RabbitMQConfig : IDisposable
    {

        private ConnectionFactory ConnectionFactory { get; }
        private static IConnection _connection { get; set; }
        private static IModel _channel { get; set; }
        // private Exchanges exchange { get; }
        public string ExchangeName { get; }
        private int deliveryLimit { get; }
        private List<SubscriberInformation> subscribers { get; set; }

        // public string ExchangeName
        // {
        //     get
        //     {
        //         return (null != exchange) ? exchange.Name : string.Empty;
        //     }
        // }

        public RabbitMQConfig(IOptions<RabbitMQSettings> rabbitMqParams, SubscribersInformation subscribersInformation)
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
            ExchangeName = configParams.ExchangeName;
            deliveryLimit = configParams.DeliveryLimit;
            subscribers = subscribersInformation.GetSubscribers();
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
            if (!string.IsNullOrEmpty(ExchangeName))
            {
                string deadLetterExchangeName = RabbitMqExchangeNameFormatter.DeadLetter(ExchangeName);

                _channel.ExchangeDeclare(ExchangeName, ExchangeType.Topic);
                _channel.ExchangeDeclare(deadLetterExchangeName, ExchangeType.Topic);

                subscribers.ForEach(
                    subscriber => DeclareQueue(subscriber, ExchangeName, deadLetterExchangeName)
                );

                // domainEventsInformation.DomainEventTypes.ForEach(
                //     domainEvent => DeclareQueue(domainEvent, exchange.Name, deadLetterExchangeName));

                // foreach (Subscribers subscriber in exchange.Subscribers)
                // {
                //     Dictionary<String, Object> args = new()
                //     {
                //         {"x-dead-letter-exchange", deadLetterExchangeName},
                //         {"x-queue-type", "quorum"},
                //         {"x-delivery-limit", deliveryLimit}
                //     };
                //     var queue = _channel.QueueDeclare(subscriber.QueueName,
                //     true,
                //     false,
                //     false,
                //     args);

                //     string deadLetterQueueName = RabbitMQQueueNameFormatter.DeadLetter(subscriber.QueueName);
                //     var deadLetterQueue = _channel.QueueDeclare(deadLetterQueueName, true,
                //         false,
                //         false);

                //     _channel.QueueBind(queue, exchange.Name, subscriber.EventName);
                //     _channel.QueueBind(deadLetterQueue, deadLetterExchangeName, subscriber.EventName);
                // }
            }
        }
        private void DeclareQueue(SubscriberInformation subscriber, string exchangeName, string deadLetterExchangeName)
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

            _channel.QueueBind(queue, exchangeName, subscriber.EventName);
            _channel.QueueBind(deadLetterQueue, deadLetterExchangeName, subscriber.EventName);
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
