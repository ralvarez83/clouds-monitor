using Shared.Domain.ValueObjects;

namespace SystemAdministrator.Machines.Domain;

public class Machine(MachineId machineId,
                              MachineName machineName,
                              BackupStatus lastBackupStatus,
                              BackupDate? lastBackupTime,
                              BackupType lastBackupType,
                              BackupDate? lastRecoveryPoint,
                              VaultId vaultId,
                              SuscriptionId suscriptionId,
                              TenantId tenantId)
{
  public MachineId MachineId { get; } = machineId;
  public MachineName MachineName { get; set; } = machineName;
  public BackupStatus LastBackupStatus { get; set; } = lastBackupStatus;
  public BackupDate? LastBackupTime { get; set; } = lastBackupTime;
  public BackupType LastBackupType { get; set; } = lastBackupType;
  public BackupDate? LastRecoveryPoint { get; set; } = lastRecoveryPoint;
  public VaultId VaultId { get; } = vaultId;
  public SuscriptionId SuscriptionId { get; } = suscriptionId;
  public TenantId TenantId { get; } = tenantId;


}
