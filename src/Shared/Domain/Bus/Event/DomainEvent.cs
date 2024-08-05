using System;
using Shared.Domain.ValueObjects;

namespace Shared.Domain.Bus.Event;

public abstract class DomainEvent
{
  public string AggregateId { get; }
  public string EventId { get; }
  public string OccurredOn { get; }

  protected DomainEvent(string aggregateId, string? eventId, SimpleDate? occurredOn)
  {
    AggregateId = aggregateId;
    EventId = eventId ?? new SimpleUuid().Value.ToString();
    OccurredOn = occurredOn != null ? occurredOn.Value.ToString() : SimpleDate.Now().ToString();
  }

  public abstract string EventName();

}