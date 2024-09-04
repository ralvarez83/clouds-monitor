using Shared.Domain.Bus.Event;
using Shared.Domain.ValueObjects;

namespace SharedTest.Domain
{
  public class DomainEventFake : DomainEvent
  {

    public DomainEventFake(string id, string name, SimpleDate date, string? eventId, SimpleDate? occurredOn) : base(id, eventId, occurredOn)
    {
      Name = name;
      Date = date;
    }
    public DomainEventFake() { }
    public string Name { get; set; } = string.Empty;
    public SimpleDate Date { get; set; } = SimpleDate.Now();

    public override string EventName() => "test.create";

    public override DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId, string occurredOn)
    {
      return new DomainEventFake(aggregateId,
                                body["name"],
                                new SimpleDate(body["date"]),
                                eventId,
                                new SimpleDate(occurredOn));
    }

    public override Dictionary<string, string> ToPrimitives()
    {
      return new Dictionary<string, string> {
        { "name", Name},
        { "date", Date.ToString()}
      };
    }
  }
}