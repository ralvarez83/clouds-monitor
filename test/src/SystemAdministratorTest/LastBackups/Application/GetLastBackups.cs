using System.Collections.Immutable;
using SystemAdministrator.LastBackups.Application.BackupsGetLast;
using SystemAdministrator.LastBackups.Domain;
using Moq;
using SystemAdministrationTest.Backups.Domain;
using SystemAdministrator.LastBackups.Application.Dtos;
using SystemAdministrator.LastBackups.Application.Dtos.Transformation;
using Shared.Domain.Bus.Query;

namespace SystemAdministrationTest.Backups.Application;

public class GetLastBackups
{
  [Fact]
  public async Task GetLastBackupsShouldReturnOneBackupPerServer()
  {
    ImmutableList<BackupDto> backupsQueryReturn = BackupsDtoFactory.BuildArrayOfBackupDtosRandom();
    Mock<QueryBus> moviRepoMok = new Mock<QueryBus>();

    moviRepoMok.Setup(_ => _.Ask<ImmutableList<BackupDto>>(It.IsAny<Query>())).ReturnsAsync(backupsQueryReturn);

    BackupsGetLast backupsGetLast = new BackupsGetLast(moviRepoMok.Object);


    ImmutableList<Backup> backupsApplicationReturn = await backupsGetLast.Search();

    string[] serverNames = backupsApplicationReturn.Select(backup => backup.name.Value).ToArray<string>();
    Assert.Equal(backupsApplicationReturn.Count, serverNames.GroupBy(backupName => backupName).Count());
  }

  [Fact]
  public async Task GetLastBackupsShouldReturnServerDataWithoutChanges()
  {
    ImmutableList<BackupDto> backupsQueryReturn = BackupsDtoFactory.BuildArrayOfBackupDtosRandom();
    Mock<QueryBus> moviRepoMok = new Mock<QueryBus>();

    moviRepoMok.Setup(_ => _.Ask<ImmutableList<BackupDto>>(It.IsAny<Query>())).ReturnsAsync(backupsQueryReturn);

    BackupsGetLast backupsGetLast = new BackupsGetLast(moviRepoMok.Object);


    ImmutableList<Backup> backupsApplicationReturn = await backupsGetLast.Search();

    string[] serverNames = backupsApplicationReturn.Select(backup => backup.name.Value).ToArray<string>();

    foreach (Backup lastBackup in backupsApplicationReturn)
    {
      Assert.Contains(BackupToBackupDTOTransformation.Run(lastBackup), backupsQueryReturn);
    }
  }
}