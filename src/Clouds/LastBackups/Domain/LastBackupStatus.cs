using Clouds.LastBackups.Domain.ValueObjects;
using Shared.Domain.Aggregate;
using Shared.Domain.Bus.Event;

namespace Clouds.LastBackups.Domain;

public class LastBackupStatus(BackupId id,
                              MachineId machineId,
                              MachineName machineName,
                              BackupStatus status,
                              BackupDate? backupTime,
                              BackupType backupType,
                              BackupDate? lastRecoveryPoint,
                              VaultId vaultId,
                              SuscriptionId suscriptionId,
                              TenantId tenantId) : AggregateRoot
{
  public BackupId Id { get; } = id;
  public MachineId MachineId { get; } = machineId;
  public MachineName MachineName { get; } = machineName;
  public BackupStatus Status { get; } = status;
  public BackupDate? BackupTime { get; } = backupTime;
  public BackupType BackupType { get; } = backupType;
  public BackupDate? LastRecoveryPoint { get; } = lastRecoveryPoint;
  public VaultId VaultId { get; } = vaultId;
  public SuscriptionId SuscriptionId { get; } = suscriptionId;
  public TenantId TenantId { get; } = tenantId;

  public static LastBackupStatus Create(BackupId id,
                              MachineId machineId,
                              MachineName machineName,
                              BackupStatus status,
                              BackupDate? backupTime,
                              BackupType backupType,
                              BackupDate? lastRecoveryPoint,
                              VaultId vaultId,
                              SuscriptionId suscriptionId,
                              TenantId tenantId)
  {
    LastBackupStatus lastBackupStatus =
      new LastBackupStatus(id, machineId, machineName, status, backupTime, backupType, lastRecoveryPoint,
                          vaultId, suscriptionId, tenantId);

    lastBackupStatus.Record(
      new LastBackupStatusDomainEvent(lastBackupStatus.Id.Value.ToString(), lastBackupStatus.MachineId.Value,
                                      lastBackupStatus.MachineName.Value, lastBackupStatus.Status.ToString(),
                                      lastBackupStatus.BackupTime != null ? lastBackupStatus.BackupTime.ToString() : string.Empty,
                                      lastBackupStatus.BackupType.ToString(),
                                      lastBackupStatus.LastRecoveryPoint != null ? lastBackupStatus.LastRecoveryPoint.ToString() : string.Empty,
                                      lastBackupStatus.VaultId.Value, lastBackupStatus.SuscriptionId.Value, lastBackupStatus.TenantId.Value));

    return lastBackupStatus;
  }

}
