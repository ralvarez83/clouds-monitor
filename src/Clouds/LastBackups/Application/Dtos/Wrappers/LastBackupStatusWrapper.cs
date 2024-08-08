using Shared.Domain.Dtos;
using Clouds.LastBackups.Domain.ValueObjects;

namespace Clouds.LastBackups.Application.Dtos.Wrappers;

public class LastBackupStatusWrapper : DomainWrapperFromDto<Domain.LastBackupStatus, LastBackupStatusDto>
{
  public static Domain.LastBackupStatus FromDto(LastBackupStatusDto backup)
  {
    return Domain.LastBackupStatus.Create(
      id: new BackupId(new Guid(backup.Id)),
      machineId: new CloudMachineId(backup.MachineId),
      machineName: new CloudMachineName(backup.MachineName),
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