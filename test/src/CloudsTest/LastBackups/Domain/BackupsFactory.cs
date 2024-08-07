using System.Collections.Immutable;
using AutoFixture;
using Clouds.LastBackups.Domain;

namespace CloudsTest.LastBackups.Domain;

public class BackupsFactory
{
  public static ImmutableList<LastBackupStatus> BuildArrayOfBackupsRandom()
  {
    Fixture fixture = new Fixture();

    return fixture.CreateMany<LastBackupStatus>().ToImmutableList();
  }

  public static ImmutableList<LastBackupStatus> BuildArrayOfBackupsEmpty()
  {
    return [];
  }

  public static LastBackupStatus BuildBackupRandom()
  {
    Fixture fixture = new Fixture();

    return fixture.Create<LastBackupStatus>();
  }
}