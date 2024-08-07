using SystemAdministrator.LastBackups.Domain;
using SystemAdministrator.LastBackups.Domain.ValueObjects;

namespace SystemAdministrator.LastBackups.Application.Dtos.Transformation;

public class BackupDtoToBackupTransformation
{
  public static Backup Run(BackupDto backup)
  {
    return new Backup(
      new BackupId(new Guid(backup.Id)),
      new CloudBackupId(backup.CloudBackupId),
      new BackupName(backup.name),
      new BackupStatus(backup.Status),
      backup.StartDate.HasValue ? new BackupDate(backup.StartDate.Value) : null,
      backup.EndtDate.HasValue ? new BackupDate(backup.EndtDate.Value) : null
    );
  }
}