using Shared.Domain.ValueObjects;
using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrator.LastBackups.Application.Register
{
  public class RegisterBackup(LastBackupsRepository repository)
  {
    private readonly LastBackupsRepository repository = repository;

    public async void Register(Machine newBackup)
    {
      Machine? backup = await this.repository.GetById(newBackup.MachineId);

      if (null != backup)
      {
        if ((null == backup.LastBackupTime && null != newBackup.LastBackupTime) ||
            (null != backup.LastBackupTime && null != newBackup.LastBackupTime && backup.LastBackupTime.Value.CompareTo(newBackup.LastBackupTime.Value) < 0))
        {
          backup.LastBackupTime = new BackupDate(newBackup.LastBackupTime.Value);
          backup.LastBackupType = newBackup.LastBackupType.Copy();
          backup.LastBackupStatus = newBackup.LastBackupStatus.Copy();
          if (null != newBackup.LastRecoveryPoint)
            backup.LastRecoveryPoint = new BackupDate(newBackup.LastRecoveryPoint.Value);

          this.repository.Save(backup);
        }
      }
      else
        this.repository.Save(newBackup);

    }
  }
}