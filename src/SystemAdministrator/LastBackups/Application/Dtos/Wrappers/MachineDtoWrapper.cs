using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrator.LastBackups.Application.Dtos.Wrappers;

public class MachineDtoWrapper
{
  public static BackupDto FromDomain(Machine backup)
  {
    return new BackupDto(
      machineId: backup.MachineId.Value,
      machineName: backup.MachineName.Value,
      status: backup.LastBackupStatus.ToString(),
      backupTime: null != backup.LastBackupTime ? backup.LastBackupTime.Value : null,
      backupType: backup.LastBackupType.ToString(),
      lastRecoveryPoint: null != backup.LastRecoveryPoint ? backup.LastRecoveryPoint.Value : null,
      vaultId: backup.VaultId.Value,
      suscriptionId: backup.SuscriptionId.Value,
      TenantId: backup.TenantId.Value);
  }
}