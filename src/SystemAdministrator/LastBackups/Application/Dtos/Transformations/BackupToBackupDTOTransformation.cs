using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrator.LastBackups.Application.Dtos.Transformation;

public class BackupToBackupDTOTransformation
{
  public static BackupDto Run(Backup backup)
  {
    return new BackupDto(
      backup.id.Value.ToString(),
      backup.cloudBackupId.Value,
      backup.name.Value,
      backup.status.Value,
      null != backup.startTime ? backup.startTime.Value : null,
      null != backup.endTime ? backup.endTime.Value : null);
  }
}