using Shared.Domain.Machines.Domain;
using Shared.Domain.ValueObjects;
using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrator.LastBackups.Application.Dtos.Wrappers;

public class MachineWrapper
{
  public static Machine FromDto(BackupDto backup)
  {
    return new Machine(
      machineId: new MachineId(backup.machineId),
      machineName: new MachineName(backup.machineName),
      lastBackupStatus: BackupStatus.Parse(backup.status),
      lastBackupTime: backup.backupTime.HasValue ? new BackupDate(backup.backupTime.Value) : null,
      lastBackupType: BackupType.Parse(backup.backupType),
      lastRecoveryPoint: backup.lastRecoveryPoint.HasValue ? new BackupDate(backup.lastRecoveryPoint.Value) : null,
      vaultId: new VaultId(backup.vaultId),
      suscriptionId: new SuscriptionId(backup.suscriptionId),
      tenantId: new TenantId(backup.TenantId)
    );
  }
  public static Machine FromDomainEntity(LastBackupStatusDomainEvent machine)
  {
    return new Machine(
      machineId: new MachineId(machine.Id),
      machineName: new MachineName(machine.MachineName),
      lastBackupStatus: BackupStatus.Parse(machine.Status),
      lastBackupTime: new BackupDate(machine.BackupTime),
      lastBackupType: BackupType.Parse(machine.BackupType),
      lastRecoveryPoint: new BackupDate(machine.LastRecoveryPoint),
      vaultId: new VaultId(machine.VaultId),
      suscriptionId: new SuscriptionId(machine.SuscriptionId),
      tenantId: new TenantId(machine.TenantId)
    );
  }
}