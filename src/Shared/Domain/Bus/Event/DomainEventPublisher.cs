using Shared.Domain.ValueObjects;

namespace Shared.Domain.Bus.Event
{
  public abstract class DomainEventPublisher : DomainEvent
  {
    protected DomainEventPublisher(string id, string? eventId, SimpleDate? occurredOn) : base(id, eventId, occurredOn) { }
    public abstract Dictionary<string, string> ToPrimitives();
  }
}