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
  public MachineName MachineName { get; set; } = machineName;
  public BackupStatus Status { get; set; } = status;
  public BackupDate? BackupTime { get; set; } = backupTime;
  public BackupType BackupType { get; set; } = backupType;
  public BackupDate? LastRecoveryPoint { get; set; } = lastRecoveryPoint;
  public VaultId VaultId { get; } = vaultId;
  public SuscriptionId SuscriptionId { get; } = suscriptionId;
  public TenantId TenantId { get; } = tenantId;


}
