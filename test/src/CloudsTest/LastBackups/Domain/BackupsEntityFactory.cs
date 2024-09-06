using System.Collections.Immutable;
using Clouds.LastBackups.Infrastructure.Repository.MongoDB;

namespace CloudsTest.LastBackups.Domain;

public class BackupsEntityFactory
{
  public static ImmutableList<LastBackupsStatusEntity> BuildArrayOfBackupEntitiesRandom()
  {
    return BackupsFactory.BuildArrayOfBackupsRandom().Select(LastBackupsStatusEntity.FromDomain).ToImmutableList();
  }

  public static ImmutableList<LastBackupsStatusEntity> BuildArrayOfBackupEntitiesEmpty()
  {
    return [];
  }

  public static LastBackupsStatusEntity BuildBackupEntityRandom()
  {
    return LastBackupsStatusEntity.FromDomain(BackupsFactory.BuildBackupRandom());
  }
}