using System.Collections.Immutable;
using SystemAdministrator.LastBackups.Application.Dtos;
using SystemAdministrator.LastBackups.Application.Dtos.Transformation;

namespace SystemAdministrationTest.Backups.Domain;

public class BackupsDtoFactory
{
  public static ImmutableList<BackupDto> BuildArrayOfBackupDtosRandom()
  {
    return BackupsFactory.BuildArrayOfBackupsRandom().Select(BackupToBackupDTOTransformation.Run).ToImmutableList();
  }

  public static ImmutableList<BackupDto> BuildArrayOfBackupDtosEmpty()
  {
    return new List<BackupDto>().ToImmutableList();
  }

  public static BackupDto BuildBackupDtoRandom()
  {
    return BackupToBackupDTOTransformation.Run(BackupsFactory.BuildBackupRandom());
  }
}