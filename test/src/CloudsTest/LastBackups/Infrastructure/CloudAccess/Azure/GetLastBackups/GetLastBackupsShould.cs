namespace CloudsTest.LastBackups.Infrastructure.CloudAccess.Azure.GetCloudLastBackups
{
  public class GetLastBackupsShould : AzureTestUnitCase
  {
    public GetLastBackupsShould() : base()
    {
    }

    [Fact]
    public async void return_backup_for_today_or_yesterday()
    {
      // Given I connect with a Tenant and a Suscripttion (Constructor and Mother class)

      // When ask for last bakcups
      _backupsReturn = await _azureAccess.GetLast();

      // Then each backups must create today or yesterday
      Assert.NotEmpty(_backupsReturn);
      int rightBackups = _backupsReturn.Where(backup =>
          null != backup.BackupTime &&
          backup.BackupTime.Value < DateTime.Today.AddDays(1) &&
          backup.BackupTime.Value >= DateTime.Today.AddDays(-1)
        ).Count();

      Assert.Equal(_backupsReturn.Count(), rightBackups);
    }

  }
}