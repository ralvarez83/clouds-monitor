using System;
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
    EventId = eventId ?? new SimpleUuid().Value.ToString();
    OccurredOn = occurredOn != null ? occurredOn.Value.ToString() : SimpleDate.Now().ToString();
  }

  public abstract string EventName();
  public abstract Dictionary<string, string> ToPrimitives();
}