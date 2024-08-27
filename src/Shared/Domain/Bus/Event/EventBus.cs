
namespace Shared.Domain.Bus.Event
{
  public interface EventBus
  {
    void Publish(List<DomainEventPublisher> domainEvents);
  }
}