using System.Collections.Immutable;
using AutoFixture;
using Shared.Domain.Machines.Domain;
using Shared.Domain.ValueObjects;
using SystemAdministrator.Machines.Domain;

namespace SystemAdministrationTest.Machines.Domain;

public class LastBackupStatusDomainEventFactory
{
  public static ImmutableList<LastBackupStatusDomainEvent> BuildArrayOfBackupDtosRandom()
  {
    return MachineFactory.BuildArrayOfBackupsRandom().Select(WrapperFromDomain).ToImmutableList();
  }

  public static ImmutableList<LastBackupStatusDomainEvent> BuildArrayOfBackupDtosEmpty()
  {
    return new List<LastBackupStatusDomainEvent>().ToImmutableList();
  }

  public static LastBackupStatusDomainEvent BuildBackupDtoRandom()
  {
    return WrapperFromDomain(MachineFactory.BuildBackupRandom());
  }

  public static LastBackupStatusDomainEvent WrapperFromDomain(Machine machine)
  {
    Fixture fixture = new Fixture();

    return new LastBackupStatusDomainEvent(machine.MachineId.Value,
                                  machine.MachineName.Value,
                                  machine.LastBackupStatus.ToString(),
                                  machine.LastBackupTime.ToString(),
                                  machine.LastBackupType.ToString(),
                                  machine.LastRecoveryPoint.ToString(),
                                  machine.VaultId.Value,
                                  machine.SuscriptionId.Value,
                                  machine.TenantId.Value,
                                  fixture.Create<string>(),
                                  SimpleDate.Now()
    );
  }
}