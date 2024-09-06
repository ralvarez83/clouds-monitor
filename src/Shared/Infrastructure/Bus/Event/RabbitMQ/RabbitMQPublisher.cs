using System.Text;
using RabbitMQ.Client;

// Base on C#-ddd-scheleton by CodelyTV: https://github.com/CodelyTV/csharp-ddd-skeleton
namespace Shared.Infrastructure.Bus.Event.RabbitMQ
{
  public class RabbitMQPublisher(RabbitMQConfig config)
  {
    private readonly RabbitMQConfig config = config;

    public void Publish(string eventName, string message)
    {
      byte[] body = Encoding.UTF8.GetBytes(message);
      Publish(config.ExchangeName, eventName, body);
    }

    public void Publish(string exchangeName, string eventName, byte[] body, int reDelivery = 0)
    {
      IModel channel = config.Channel();

      channel.BasicPublish(exchange: exchangeName,
                           routingKey: eventName,
                           basicProperties: null,
                           body: body);
    }
  }
}