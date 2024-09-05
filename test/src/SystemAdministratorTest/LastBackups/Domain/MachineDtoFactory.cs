using System.Collections.Immutable;
using SystemAdministrator.LastBackups.Application.Dtos;
using SystemAdministrator.LastBackups.Application.Dtos.Wrappers;

namespace SystemAdministrationTest.Backups.Domain;

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