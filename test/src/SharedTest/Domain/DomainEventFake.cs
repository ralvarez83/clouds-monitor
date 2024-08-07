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

    public string Name { get; }
    public SimpleDate Date { get; }

    public override string EventName() => "test.create";

    public override Dictionary<string, string> ToPrimitives()
    {
      return new Dictionary<string, string> {
        { "name", Name},
        { "date", Date.ToString()}
      };
    }
  }
}