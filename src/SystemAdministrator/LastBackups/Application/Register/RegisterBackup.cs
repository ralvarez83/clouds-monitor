using Shared.Domain.ValueObjects;
using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrator.LastBackups.Application.Register
{
  public class RegisterBackup(BackupsRepository repository)
  {
    private readonly BackupsRepository repository = repository;

    public async void Register(Backup newBackup)
    {
      Backup? backup = await this.repository.GetById(newBackup.MachineId);

      if (null != backup)
      {
        if ((null == backup.BackupTime && null != newBackup.BackupTime) ||
            (null != backup.BackupTime && null != newBackup.BackupTime && backup.BackupTime.Value.CompareTo(newBackup.BackupTime.Value) < 0))
        {
          backup.BackupTime = new BackupDate(newBackup.BackupTime.Value);
          backup.BackupType = newBackup.BackupType.Copy();
          backup.Status = newBackup.Status.Copy();
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