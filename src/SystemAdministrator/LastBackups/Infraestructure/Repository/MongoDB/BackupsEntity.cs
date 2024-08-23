using Shared.Domain.ValueObjects;
using Shared.Infrastructure.Repository.EntityFramework;
using SystemAdministrator.LastBackups.Domain;
using BackupTypeType = Shared.Domain.ValueObjects.BackupType;

namespace SystemAdministrator.LastBackups.Infrastructure.Repository.MongoDB
{
  public class BackupsEntity : Entity
  {

    public string Id { get; set; } = string.Empty;
    public string MachineName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime? BackupTime { get; set; }
    public string BackupType { get; set; } = string.Empty;
    public DateTime? LastRecoveryPoint { get; set; }
    public string VaultId { get; set; } = string.Empty;
    public string SuscriptionId { get; set; } = string.Empty;
    public string TenantId { get; set; } = string.Empty;

    public static BackupsEntity FromDomain(Backup backup)
    {
      return new BackupsEntity
      {
        Id = backup.MachineId.Value,
        MachineName = backup.MachineName.Value,
        Status = backup.Status.ToString(),
        BackupTime = null != backup.BackupTime ? backup.BackupTime.Value : null,
        BackupType = backup.BackupType.ToString(),
        LastRecoveryPoint = null != backup.LastRecoveryPoint ? backup.LastRecoveryPoint.Value : null,
        VaultId = backup.VaultId.Value,
        SuscriptionId = backup.SuscriptionId.Value,
        TenantId = backup.TenantId.Value
      };
    }

    public static Backup ToDomain(BackupsEntity entity)
    {
      return new Backup(
        machineId: new(entity.Id),
        new(entity.MachineName),
        BackupStatus.Parse(entity.Status),
        entity.BackupTime.HasValue ? new(entity.BackupTime.Value) : null,
        BackupTypeType.Parse(entity.BackupType),
        entity.LastRecoveryPoint.HasValue ? new(entity.LastRecoveryPoint.Value) : null,
        new(entity.VaultId),
        new(entity.SuscriptionId),
        new(entity.TenantId)
      );
    }
  }
}