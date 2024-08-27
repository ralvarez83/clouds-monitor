using Shared.Domain.Aggregate;
using Shared.Domain.Bus.Event;
using Shared.Domain.ValueObjects;

namespace SystemAdministrator.LastBackups.Domain
{
  public class MachineDomainEvent : DomainEventSubscriber
  {
    public MachineDomainEvent(string machineId, string machineName, string lastBackupStatus,
                                        string lastBackupTime, string lastBackupType, string lastRecoveryPoint,
                                        string vaultId, string suscriptionId, string tenantId,
                                        string? eventId = null, SimpleDate? occurredOn = null) : base(machineId, eventId, occurredOn)
    {
      MachineName = machineName;
      LastBackupStatus = lastBackupStatus;
      LastBackupTime = lastBackupTime;
      LastBackupType = lastBackupType;
      LastRecoveryPoint = lastRecoveryPoint;
      VaultId = vaultId;
      SuscriptionId = suscriptionId;
      TenantId = tenantId;
    }
    public MachineDomainEvent() { }
    public string MachineName { get; }
    public string LastBackupStatus { get; }
    public string LastBackupTime { get; }
    public string LastBackupType { get; }
    public string LastRecoveryPoint { get; }
    public string VaultId { get; }
    public string SuscriptionId { get; }
    public string TenantId { get; }

    public Machine Domain => new Machine(new MachineId(Id),
                                            new MachineName(MachineName),
                                            BackupStatus.Parse(LastBackupStatus),
                                            new BackupDate(LastBackupTime),
                                            BackupType.Parse(LastBackupType),
                                            new BackupDate(LastRecoveryPoint),
                                            new VaultId(VaultId),
                                            new SuscriptionId(SuscriptionId),
                                            new TenantId(TenantId));

    public override string EventName() => "lastbackup.created";

    public override DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId, string occurredOn)
    {
      return new MachineDomainEvent(aggregateId,
                                            body["machine_name"],
                                            body["status"],
                                            body["backup_time"],
                                            body["backup_type"],
                                            body["last_recovery_point"],
                                            body["vault_id"],
                                            body["suscription_id"],
                                            body["tenant_id"],
                                            eventId,
                                            new SimpleDate(occurredOn));
    }

  }
}