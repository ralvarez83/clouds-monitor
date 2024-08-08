using System.Collections.Immutable;
using Moq;
using Clouds.LastBackups.Application.Dtos;
using CloudsTest.LastBackups.Domain;
using CloudsTest.LastBackups.Infrastructure;
using Clouds.LastBackups.Domain;
using Clouds.LastBackups.Application.UpdateLastBackups;
using Clouds.LastBackups.Application.GetCloudLast;
using Clouds.LastBackups.Domain.ValueObjects;
using Clouds.LastBackups.Application.Dtos.Wrappers;
using Shared.Domain.Criteria;
using Shared.Domain.Criteria.Filters;

namespace CloudsTest.LastBackups.Application.RecoverLastBackups
{
  public class UpdateLastBackupsHandlerShould : BackupsUnitTestCase
  {

    private UpdateLastBackupsHandler _handler;

    public UpdateLastBackupsHandlerShould()
    {
      _handler = new UpdateLastBackupsHandler(new UpdateLastBackups(_repository.Object, _queryBusMok.Object, _eventBusMok.Object));
    }
    [Fact]
    public async void Nothing_if_not_get_backups()
    {

      // Given cloud doesn't have backups and the Data Base is empty
      ImmutableList<LastBackupStatusDto> backupsInCloud = BackupsDtoFactory.BuildArrayOfBackupDtosEmpty();
      ConfigureGetCloudLastBackups(backupsInCloud);

      //When last backups are been updated
      await _handler.Handle(new UpdateLastBackupsCommand());

      ShouldHaveSave(0);
      ShouldHaveNotPublished();
    }


    /// <summary>
    /// A revisar de aquí para abajo
    /// </summary>
    [Fact]
    public async void Found_all_new_backups()
    {

      // Given cloud doesn't have backups and the Data Base is empty
      ImmutableList<LastBackupStatusDto> backupsInCloud = BackupsDtoFactory.BuildArrayOfBackupDtosRandom();
      ConfigureGetCloudLastBackups(backupsInCloud);
      ConfigureGetRepositoryLastBackups([.. (new List<LastBackupStatus>())]);

      //When last backups are been updated
      await _handler.Handle(new UpdateLastBackupsCommand());

      // Then a backup was saved and event has been published
      ShouldHaveSave(backupsInCloud.Count);
      ShouldHavePublished(backupsInCloud.Count, 1);
    }


    [Fact]
    public async Task Found_one_backup_in_repository()
    {
      // Given find one backup updated in repository
      ImmutableList<LastBackupStatusDto> backupsQueryReturn = BackupsDtoFactory.BuildArrayOfBackupDtosRandom();
      ConfigureGetCloudLastBackups(backupsQueryReturn);

      LastBackupStatusDto backupSaved = backupsQueryReturn.Last();
      List<LastBackupStatus> listBackupSaved = [LastBackupStatusWrapper.FromDto(backupSaved)];
      ConfigureGetRepositoryLastBackups(listBackupSaved.ToImmutableList());

      //When last backups are been updated
      await _handler.Handle(new UpdateLastBackupsCommand());

      //Then all backups less found it is saved and events have been published
      ShouldHaveSave(backupsQueryReturn.Count - 1);
      ShouldHavePublished(backupsQueryReturn.Count - 1, 1);
    }


    [Fact]
    public async Task Found_many_backups_in_repository()
    {
      // Given find half of the backups updated in repository
      ImmutableList<LastBackupStatusDto> backupsQueryReturn = BackupsDtoFactory.BuildArrayOfBackupDtosRandom();
      ConfigureGetCloudLastBackups(backupsQueryReturn);

      int half = backupsQueryReturn.Count() / 2;
      Random rnd = new Random();
      ImmutableList<LastBackupStatus> listBackupSaved = backupsQueryReturn.OrderBy(x => rnd.Next()).Take(half).Select(LastBackupStatusWrapper.FromDto).ToImmutableList();

      ConfigureGetRepositoryLastBackups(listBackupSaved);

      //When last backups are been updated
      await _handler.Handle(new UpdateLastBackupsCommand());

      //Then all backups less found it is saved and events have been published
      ShouldHaveSave(backupsQueryReturn.Count - half);
      ShouldHavePublished(backupsQueryReturn.Count - half, 1);

    }

    private void ConfigureGetCloudLastBackups(ImmutableList<LastBackupStatusDto> backupsInCloud)
    {
      _queryBusMok
                .Setup(_ => _.Ask<ImmutableList<LastBackupStatusDto>>(It.IsAny<GetCloudLastBackupsQuery>()))
                .ReturnsAsync(backupsInCloud);
    }

    private void ConfigureGetRepositoryLastBackups(ImmutableList<LastBackupStatus> backupsInCloud)
    {
      _repository
        .Setup(_ => _.Search(
          It.Is<Criteria>(
            criteria => criteria.filters.FiltersFiled.First().field == MachineId.GetName() &&
                        criteria.filters.FiltersFiled.First().fieldOperator == FilterOperator.In)))
        .Returns<Criteria>((criteria) =>
                {
                  return Task.Run(
                    () => backupsInCloud.Where(backup => criteria.filters.FiltersFiled.First().value.Contains(backup.MachineId.Value)).ToImmutableList());
                }
        );
    }
  }
}