using Shared.Domain.Bus.Event;

// Base on C#-ddd-scheleton by CodelyTV: https://github.com/CodelyTV/csharp-ddd-skeleton
namespace Shared.Infraestructure.Bus.Event.RabbitMQ
{
  public class RabbitMQEventBus(RabbitMQPublisher publisher) : EventBus
  {
    private readonly RabbitMQPublisher publisher = publisher;

    public void Publish(List<DomainEvent> domainEvents)
    {
      domainEvents.ForEach(Publish);
    }

    private void Publish(DomainEvent domainEvent)
    {
      string message = DomainEventJsonSerializer.Serialize(domainEvent);

      publisher.Publish(domainEvent.EventName(), message);
    }
  }
}