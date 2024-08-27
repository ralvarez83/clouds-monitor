using System.Collections.Immutable;
using AutoFixture;
using Shared.Domain.ValueObjects;
using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrationTest.Backups.Domain;

public class MachineDomainEnventFactory
{
  public static ImmutableList<MachineDomainEvent> BuildArrayOfBackupDtosRandom()
  {
    return MachineFactory.BuildArrayOfBackupsRandom().Select(WrapperFromDomain).ToImmutableList();
  }

  public static ImmutableList<MachineDomainEvent> BuildArrayOfBackupDtosEmpty()
  {
    return new List<MachineDomainEvent>().ToImmutableList();
  }

  public static MachineDomainEvent BuildBackupDtoRandom()
  {
    return WrapperFromDomain(MachineFactory.BuildBackupRandom());
  }

  public static MachineDomainEvent WrapperFromDomain(Machine machine)
  {
    Fixture fixture = new Fixture();

    return new MachineDomainEvent(machine.MachineId.Value,
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