using System.Collections.Immutable;
using SystemAdministrator.LastBackups.Application.Dtos;
using SystemAdministrator.LastBackups.Application.Dtos.Wrappers;

namespace SystemAdministrationTest.Backups.Domain;

public class MachineDtoFactory
{
  public static ImmutableList<BackupDto> BuildArrayOfBackupDtosRandom()
  {
    return MachineFactory.BuildArrayOfBackupsRandom().Select(MachineDtoWrapper.FromDomain).ToImmutableList();
  }

  public static ImmutableList<BackupDto> BuildArrayOfBackupDtosEmpty()
  {
    return new List<BackupDto>().ToImmutableList();
  }

  public static BackupDto BuildBackupDtoRandom()
  {
    return MachineDtoWrapper.FromDomain(MachineFactory.BuildBackupRandom());
  }
}