using System.Collections.Immutable;
using AutoFixture;
using Clouds.LastBackups.Application.Dtos;
using Clouds.LastBackups.Application.Dtos.Transformation;
using Clouds.LastBackups.Infraestructure.Repository.MongoDB;

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