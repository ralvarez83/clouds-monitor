using Shared.Domain.Aggregate;
using Shared.Domain.ValueObjects;

namespace Shared.Domain.Bus.Event
{
  public abstract class DomainEventSubscriber : DomainEvent
  {
    protected DomainEventSubscriber(string id, string? eventId, SimpleDate? occurredOn) : base(id, eventId, occurredOn) { }
    protected DomainEventSubscriber() { }
    public abstract DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId,
              string occurredOn);

  }
}