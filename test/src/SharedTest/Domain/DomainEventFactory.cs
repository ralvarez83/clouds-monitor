using System.Collections.Immutable;
using AutoFixture;
using Shared.Domain.Bus.Event;

namespace SharedTest.Domain
{
  public class DomainEventFactory
  {
    public static DomainEventFake[] BuildArrayOfBackupsRandom()
    {
      Fixture fixture = new Fixture();

      return fixture.CreateMany<DomainEventFake>().ToArray();
    }

    public static DomainEventFake BuildEventRandom()
    {
      Fixture fixture = new Fixture();

      return fixture.Create<DomainEventFake>();
    }

  }
}