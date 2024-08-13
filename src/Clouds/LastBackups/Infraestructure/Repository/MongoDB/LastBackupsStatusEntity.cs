using Clouds.LastBackups.Domain;
using Clouds.LastBackups.Infraestructure.Repository.EntityFramework;
using Shared.Domain.ValueObjects;
using BackupTypeType = Shared.Domain.ValueObjects.BackupType;

namespace Clouds.LastBackups.Infraestructure.Repository.MongoDB
{
  public class LastBackupsStatusEntity : Entity
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

    public static LastBackupsStatusEntity FromDomain(LastBackupStatus lastBackupsStatus)
    {
      return new LastBackupsStatusEntity
      {
        Id = lastBackupsStatus.MachineId.Value,
        MachineName = lastBackupsStatus.MachineName.Value,
        Status = lastBackupsStatus.Status.ToString(),
        BackupTime = null != lastBackupsStatus.BackupTime ? lastBackupsStatus.BackupTime.Value : null,
        BackupType = lastBackupsStatus.BackupType.ToString(),
        LastRecoveryPoint = null != lastBackupsStatus.LastRecoveryPoint ? lastBackupsStatus.LastRecoveryPoint.Value : null,
        VaultId = lastBackupsStatus.VaultId.Value,
        SuscriptionId = lastBackupsStatus.SuscriptionId.Value,
        TenantId = lastBackupsStatus.TenantId.Value
      };
    }

    public static LastBackupStatus ToDomain(LastBackupsStatusEntity entity)
    {
      return new LastBackupStatus(
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