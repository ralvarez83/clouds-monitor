using System.Collections.Immutable;
using AutoFixture;
using Clouds.LastBackups.Application.Dtos;
using Clouds.LastBackups.Application.Dtos.Transformation;

namespace CloudsTest.LastBackups.Domain;

public class BackupsDtoFactory
{
  public static ImmutableList<LastBackupStatusDto> BuildArrayOfBackupDtosRandom()
  {
    return BackupsFactory.BuildArrayOfBackupsRandom().Select(LastBackupStatusDtoWrapper.FromDomain).ToImmutableList();
  }

  public static ImmutableList<LastBackupStatusDto> BuildArrayOfBackupDtosEmpty()
  {
    return new List<LastBackupStatusDto>().ToImmutableList();
  }

  public static LastBackupStatusDto BuildBackupDtoRandom()
  {
    return LastBackupStatusDtoWrapper.FromDomain(BackupsFactory.BuildBackupRandom());
  }
}