using Shared.Domain.ValueObjects;

namespace Shared.Domain.Bus.Event;

public abstract class DomainEvent
{
  public string Id { get; }
  public string EventId { get; }
  public string OccurredOn { get; }

  protected DomainEvent(string id, string? eventId, SimpleDate? occurredOn)
  {
    Id = id;
    EventId = eventId ?? new SimpleUuid().ToString();
    OccurredOn = occurredOn != null ? occurredOn.ToString() : SimpleDate.Now().ToString();
  }

  protected DomainEvent() { }

  public abstract string EventName();
  public abstract Dictionary<string, string> ToPrimitives();
  public abstract DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId,
            string occurredOn);
}