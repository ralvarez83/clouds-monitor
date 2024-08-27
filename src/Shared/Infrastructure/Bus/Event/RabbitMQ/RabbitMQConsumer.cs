using Clouds.LastBackups.Infraestructure.Bus.RabbitMQ;
using Shared.Domain.Bus.Event;

namespace Shared.Infrastructure.Bus.Event.RabbitMQ
{
  public class RabbitMQConsumer(RabbitMQConfig config, Subscriber suscriber) : Consumer
  {
    private readonly RabbitMQConfig config = config;
    private readonly Subscriber suscriber = suscriber;

    public Task Consume()
    {
      throw new NotImplementedException();
    }
  }
}