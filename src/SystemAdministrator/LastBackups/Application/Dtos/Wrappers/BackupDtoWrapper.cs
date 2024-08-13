using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrator.LastBackups.Application.Dtos.Wrappers;

public class BackupDtoWrapper
{
  public static BackupDto FromDomain(Backup backup)
  {
    return new BackupDto(
      machineId: backup.MachineId.Value,
      machineName: backup.MachineName.Value,
      status: backup.Status.ToString(),
      backupTime: null != backup.BackupTime ? backup.BackupTime.Value : null,
      backupType: backup.BackupType.ToString(),
      lastRecoveryPoint: null != backup.LastRecoveryPoint ? backup.LastRecoveryPoint.Value : null,
      vaultId: backup.VaultId.Value,
      suscriptionId: backup.SuscriptionId.Value,
      TenantId: backup.TenantId.Value);
  }
}