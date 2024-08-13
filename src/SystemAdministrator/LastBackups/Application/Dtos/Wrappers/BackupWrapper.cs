using Shared.Domain.ValueObjects;
using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrator.LastBackups.Application.Dtos.Wrappers;

public class BackupWrapper
{
  public static Backup FromDto(BackupDto backup)
  {
    return new Backup(
      machineId: new MachineId(backup.machineId),
      machineName: new MachineName(backup.machineName),
      status: BackupStatus.Parse(backup.status),
      backupTime: backup.backupTime.HasValue ? new BackupDate(backup.backupTime.Value) : null,
      backupType: BackupType.Parse(backup.backupType),
      lastRecoveryPoint: backup.lastRecoveryPoint.HasValue ? new BackupDate(backup.lastRecoveryPoint.Value) : null,
      vaultId: new VaultId(backup.vaultId),
      suscriptionId: new SuscriptionId(backup.suscriptionId),
      tenantId: new TenantId(backup.TenantId)
    );
  }
}