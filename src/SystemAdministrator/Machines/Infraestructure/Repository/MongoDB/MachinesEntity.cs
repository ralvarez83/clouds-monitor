using Shared.Domain.ValueObjects;
using Shared.Infrastructure.Repository.EntityFramework;
using SystemAdministrator.Machines.Domain;
using BackupTypeType = Shared.Domain.ValueObjects.BackupType;

namespace SystemAdministrator.Machines.Infrastructure.Repository.MongoDB
{
  public class MachinesEntity : Entity
  {

    public string Id { get; set; } = string.Empty;
    public string MachineName { get; set; } = string.Empty;
    public string LastBackupStatus { get; set; } = string.Empty;
    public DateTime? LastBackupTime { get; set; }
    public string BackupType { get; set; } = string.Empty;
    public DateTime? LastRecoveryPoint { get; set; }
    public string VaultId { get; set; } = string.Empty;
    public string SuscriptionId { get; set; } = string.Empty;
    public string TenantId { get; set; } = string.Empty;

    public static MachinesEntity FromDomain(Machine backup)
    {
      return new MachinesEntity
      {
        Id = backup.MachineId.Value,
        MachineName = backup.MachineName.Value,
        LastBackupStatus = backup.LastBackupStatus.ToString(),
        LastBackupTime = null != backup.LastBackupTime ? backup.LastBackupTime.Value : null,
        BackupType = backup.LastBackupType.ToString(),
        LastRecoveryPoint = null != backup.LastRecoveryPoint ? backup.LastRecoveryPoint.Value : null,
        VaultId = backup.VaultId.Value,
        SuscriptionId = backup.SuscriptionId.Value,
        TenantId = backup.TenantId.Value
      };
    }

    public static Machine ToDomain(MachinesEntity entity)
    {
      return new Machine(
        machineId: new(entity.Id),
        new(entity.MachineName),
        Shared.Domain.ValueObjects.BackupStatus.Parse(entity.LastBackupStatus),
        entity.LastBackupTime.HasValue ? new(entity.LastBackupTime.Value) : null,
        BackupTypeType.Parse(entity.BackupType),
        entity.LastRecoveryPoint.HasValue ? new(entity.LastRecoveryPoint.Value) : null,
        new(entity.VaultId),
        new(entity.SuscriptionId),
        new(entity.TenantId)
      );
    }
  }
}