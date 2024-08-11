using Shared.Domain.ValueObjects;

namespace SystemAdministrator.LastBackups.Domain;

public class Backup(MachineId machineId,
                              MachineName machineName,
                              BackupStatus status,
                              BackupDate? backupTime,
                              BackupType backupType,
                              BackupDate? lastRecoveryPoint,
                              VaultId vaultId,
                              SuscriptionId suscriptionId,
                              TenantId tenantId)
{
  public MachineId MachineId { get; } = machineId;
  public MachineName MachineName { get; } = machineName;
  public BackupStatus Status { get; } = status;
  public BackupDate? BackupTime { get; } = backupTime;
  public BackupType BackupType { get; } = backupType;
  public BackupDate? LastRecoveryPoint { get; } = lastRecoveryPoint;
  public VaultId VaultId { get; } = vaultId;
  public SuscriptionId SuscriptionId { get; } = suscriptionId;
  public TenantId TenantId { get; } = tenantId;


}
