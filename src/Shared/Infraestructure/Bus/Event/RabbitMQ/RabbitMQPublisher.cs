using System.Text;
using Clouds.LastBackups.Infraestructure.Bus.RabbitMQ;
using RabbitMQ.Client;

// Base on C#-ddd-scheleton by CodelyTV: https://github.com/CodelyTV/csharp-ddd-skeleton
namespace Shared.Infraestructure.Bus.Event.RabbitMQ
{
  public class RabbitMQPublisher(RabbitMQConfig config)
  {
    private readonly RabbitMQConfig config = config;
    private const string HeaderReDelivery = "redelivery_count";

    public void Publish(string eventName, string message)
    {
      IModel channel = config.Channel();
      channel.ExchangeDeclare(config.ExchangeName, ExchangeType.Topic);
      byte[] body = Encoding.UTF8.GetBytes(message);

      IBasicProperties properties = channel.CreateBasicProperties();
      properties.Headers = new Dictionary<string, object>
            {
                {HeaderReDelivery, 0}
            };

      channel.BasicPublish(exchange: config.ExchangeName,
                           routingKey: eventName,
                           basicProperties: null,
                           body: body);
    }
  }
}