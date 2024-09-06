using System.Collections.Immutable;
using AutoFixture;
using SystemAdministrator.Machines.Domain;

namespace SystemAdministrationTest.Machines.Domain;

public class MachineFactory
{
  public static ImmutableList<Machine> BuildArrayOfBackupsRandom()
  {
    Fixture fixture = new Fixture();

    return fixture.CreateMany<Machine>().ToImmutableList<Machine>();
  }

  public static Machine BuildBackupRandom()
  {
    Fixture fixture = new Fixture();

    return fixture.Create<Machine>();
  }
}