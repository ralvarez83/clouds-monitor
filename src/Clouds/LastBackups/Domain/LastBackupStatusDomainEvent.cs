using Clouds.LastBackups.Domain.ValueObjects;
using Shared.Domain.Bus.Event;
using Shared.Domain.ValueObjects;

namespace Clouds.LastBackups.Domain
{
  public class LastBackupStatusDomainEvent : DomainEvent
  {
    public LastBackupStatusDomainEvent(string machineId, string machineName, string status,
                                        string backupTime, string backupType, string lastRecoveryPoint,
                                        string vaultId, string suscriptionId, string tenantId,
                                        string? eventId = null, SimpleDate? occurredOn = null) : base(machineId, eventId, occurredOn)
    {
      MachineName = machineName;
      Status = status;
      BackupTime = backupTime;
      BackupType = backupType;
      LastRecoveryPoint = lastRecoveryPoint;
      VaultId = vaultId;
      SuscriptionId = suscriptionId;
      TenantId = tenantId;
    }
    public string MachineName { get; }
    public string Status { get; }
    public string BackupTime { get; }
    public string BackupType { get; }
    public string LastRecoveryPoint { get; }
    public string VaultId { get; }
    public string SuscriptionId { get; }
    public string TenantId { get; }

    public override string EventName() => "lastbackup.created";

    public override Dictionary<string, string> ToPrimitives()
    {
      return new Dictionary<string, string>
            {
              {"machine_name", MachineName},
              {"status", Status },
              {"backup_time", BackupTime },
              {"backup_type", BackupType },
              {"last_recovery_point", LastRecoveryPoint },
              {"vault_id", VaultId },
              {"suscription_id", SuscriptionId },
              {"tenant_id", TenantId }
            };
    }
  }
}