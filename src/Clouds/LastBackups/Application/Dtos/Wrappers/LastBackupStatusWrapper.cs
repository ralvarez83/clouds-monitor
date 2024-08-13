using Shared.Domain.Dtos;
using Shared.Domain.ValueObjects;

namespace Clouds.LastBackups.Application.Dtos.Wrappers;

public class LastBackupStatusWrapper : DomainWrapperFromDto<Domain.LastBackupStatus, LastBackupStatusDto>
{
  public static Domain.LastBackupStatus FromDto(LastBackupStatusDto backup)
  {
    return Domain.LastBackupStatus.Create(
      machineId: new MachineId(backup.MachineId),
      machineName: new MachineName(backup.MachineName),
      status: BackupStatus.Parse(backup.Status),
      backupTime: backup.BackupTime.HasValue ? new BackupDate(backup.BackupTime.Value) : null,
      backupType: BackupType.Parse(backup.BackupType),
      lastRecoveryPoint: backup.LastRecoveryPoint.HasValue ? new BackupDate(backup.LastRecoveryPoint.Value) : null,
      vaultId: new VaultId(backup.VaultId),
      suscriptionId: new SuscriptionId(backup.SuscriptionId),
      tenantId: new TenantId(backup.TenantId)

    );
  }
}