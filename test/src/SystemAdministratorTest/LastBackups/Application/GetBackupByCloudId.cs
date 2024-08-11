
using System.Collections.Immutable;
using Moq;
using Shared.Backups.Application.BackupsGetLastNDays;
using SystemAdministrator.LastBackups.Domain;
using SystemAdministrator.LastBackups.Domain.ValueObjects;
using Shared.Domain.Criteria;
using Shared.Domain.Criteria.Filters;
using SystemAdministrationTest.Backups.Domain;

namespace SystemAdministrationTest.Backups.Application
{
  public class GetBackupByCloudId
  {
    private Mock<LastBackupsRepository> repositoryMok = new Mock<LastBackupsRepository>();

    [Fact]
    public async Task FoundBackup()
    {
      Backup backup = BackupsFactory.BuildBackupRandom();
      // Given
      GivenDBHasTheBackup(backup);
      // When
      Backup? backupReturned = await WhenSearchByCloudId(backup.cloudBackupId);
      // Then
      ThenBackupReturnedShouldBeSameToBackupInDB(backup, backupReturned);
    }

    [Fact]
    public async Task NotFoundBackup()
    {
      Backup backup = BackupsFactory.BuildBackupRandom();
      // Given
      GivenDBHasNotTheBackup(backup);
      // When
      Backup? backupReturned = await WhenSearchByCloudId(backup.cloudBackupId);
      // Then
      ThenBackupReturnedShouldBeSameToBackupInDB(null, backupReturned);
    }

    private void GivenDBHasNotTheBackup(Backup backup)
    {
      List<Backup> backupsReturned = new List<Backup>();
      repositoryMok.Setup(_ => _.SearchByCriteria(
        It.Is<Criteria>(criteriaReceive =>
          criteriaReceive.filters.FiltersFiled.Where(filter =>
            filter.field == Filter.CLOUD_ID && filter.fieldOperator == FilterOperator.Equal && filter.value == backup.cloudBackupId.Value
          ).Count() == 1)
        )
      ).ReturnsAsync(backupsReturned.ToImmutableList());
    }

    private void GivenDBHasTheBackup(Backup backup)
    {
      List<Backup> backupsReturned = new List<Backup>();
      backupsReturned.Add(backup);

      repositoryMok.Setup(_ => _.SearchByCriteria(
        It.Is<Criteria>(criteriaReceive =>
          criteriaReceive.filters.FiltersFiled.Where(filter =>
            filter.field == Filter.CLOUD_ID && filter.fieldOperator == FilterOperator.Equal && filter.value == backup.cloudBackupId.Value
          ).Count() == 1)
        )
      ).ReturnsAsync(backupsReturned.ToImmutableList());

    }
    private async Task<Backup?> WhenSearchByCloudId(CloudBackupId cloudBackupId)
    {
      BackupsGetByCloudId backupsGetByCloudId = new BackupsGetByCloudId(repositoryMok.Object);

      Backup? backup = (await backupsGetByCloudId.Search(cloudBackupId)).FirstOrDefault<Backup?>();

      return backup;
    }
    private void ThenBackupReturnedShouldBeSameToBackupInDB(Backup? backupInDB, Backup? backupReturned)
    {
      Assert.Equal(backupInDB, backupReturned);
    }
  }

}