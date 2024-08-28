using System.Collections.Immutable;
using AutoFixture;
using Shared.Domain.Bus.Event;
using Shared.Domain.ValueObjects;

namespace SharedTest.Domain
{
  public class DomainEventFactory
  {
    public static DomainEventFake[] BuildArrayOfBackupsRandom()
    {
      Fixture fixture = new Fixture();
      return fixture.Build<DomainEventFake>()
        .FromFactory<int>((x) => BuildEventRandom())
        .CreateMany().ToArray();
    }

    public static DomainEventFake BuildEventRandom()
    {
      Fixture fixture = new Fixture();

      return new DomainEventFake(
                                  fixture.Create<Guid>().ToString(),
                                  fixture.Create<string>(),
                                  fixture.Create<SimpleDate>(),
                                  fixture.Create<Guid>().ToString(),
                                  fixture.Create<SimpleDate>()
                                );
    }

  }
}