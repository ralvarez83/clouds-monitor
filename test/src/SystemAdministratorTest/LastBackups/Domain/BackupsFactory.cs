using System.Collections.Immutable;
using AutoFixture;
using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrationTest.Backups.Domain;

public class BackupsFactory
{
  public static ImmutableList<Backup> BuildArrayOfBackupsRandom()
  {
    Fixture fixture = new Fixture();

    return fixture.CreateMany<Backup>().ToImmutableList<Backup>();
  }

  public static Backup BuildBackupRandom()
  {
    Fixture fixture = new Fixture();

    return fixture.Create<Backup>();
  }
}