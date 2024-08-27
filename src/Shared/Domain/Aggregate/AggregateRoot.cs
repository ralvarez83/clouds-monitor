using Shared.Domain.Bus.Event;

namespace Shared.Domain.Aggregate
{
  public abstract class AggregateRoot
  {
    private List<DomainEventPublisher> _domainEvents = new List<DomainEventPublisher>();

    public List<DomainEventPublisher> PullDomainEvents()
    {
      List<DomainEventPublisher> events = _domainEvents;

      _domainEvents = new List<DomainEventPublisher>();

      return events;
    }

    protected void Record(DomainEventPublisher domainEvent)
    {
      _domainEvents.Add(domainEvent);
    }


  }
}