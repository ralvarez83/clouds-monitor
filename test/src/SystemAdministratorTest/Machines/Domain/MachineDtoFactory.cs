using System.Collections.Immutable;
using SystemAdministrator.Machines.Application.Dtos;
using SystemAdministrator.Machines.Application.Dtos.Wrappers;

namespace SystemAdministrationTest.Machines.Domain;

public class MachineDtoFactory
{
  public static ImmutableList<MachineDto> BuildArrayOfBackupDtosRandom()
  {
    return MachineFactory.BuildArrayOfBackupsRandom().Select(MachineDtoWrapper.FromDomain).ToImmutableList();
  }

  public static ImmutableList<MachineDto> BuildArrayOfBackupDtosEmpty()
  {
    return new List<MachineDto>().ToImmutableList();
  }

  public static MachineDto BuildBackupDtoRandom()
  {
    return MachineDtoWrapper.FromDomain(MachineFactory.BuildBackupRandom());
  }
}