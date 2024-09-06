using Clouds.LastBackups.Domain;
using Clouds.LastBackups.Infrastructure.Repository.MongoDB;
using CloudsTest.LastBackups.Domain;
using Shared.Domain.ValueObjects;

namespace CloudsTest.LastBackups.Infrastructure.MongoDB
{
  public class SaveBackupShould : CloudMongoDBUnitCase
  {

    [Fact]
    public void Add_new_LastBackupStatus()
    {
      // Given database is empty and I have a new LastBackupStatus
      LastBackupStatus lastBackupStatus = BackupsFactory.BuildBackupRandom();

      // When save the new element
      using (LastBackupsStatusContext testContext = GetTemporalDBContext())
      {
        MongoDBLastBackupRepository repository = new MongoDBLastBackupRepository(testContext);
        repository.Save(lastBackupStatus);
      }

      // Then the element is in the daba base
      using (LastBackupsStatusContext thenContext = GetTemporalDBContext())
      {
        Assert.NotEmpty(thenContext.LastBackupStatus);
        DropDataBase();
      }
    }

    [Fact]
    public void Update_existing_LastBackupStatus()
    {
      // Given database has one LastBackupStatus
      LastBackupStatus lastBackupStatus = BackupsFactory.BuildBackupRandom();
      using (LastBackupsStatusContext existingDataContext = GetTemporalDBContext())
      {
        existingDataContext.LastBackupStatus.Add(LastBackupsStatusEntity.FromDomain(lastBackupStatus));
        existingDataContext.SaveChanges();
      }

      // When a field change and we save the element
      BackupDate newBackupDate = new BackupDate(DateTime.Now);
      LastBackupStatus lastBackupStatusUpdated = new(new MachineId(lastBackupStatus.MachineId.Value),
                                                    new MachineName(lastBackupStatus.MachineName.Value),
                                                    BackupStatus.Parse(lastBackupStatus.Status.ToString()),
                                                    newBackupDate,
                                                    BackupType.Parse(lastBackupStatus.BackupType.ToString()),
                                                    null != lastBackupStatus.LastRecoveryPoint ? new BackupDate(lastBackupStatus.LastRecoveryPoint.Value) : null,
                                                    new VaultId(lastBackupStatus.VaultId.Value),
                                                    new SuscriptionId(lastBackupStatus.SuscriptionId.Value),
                                                    new TenantId(lastBackupStatus.TenantId.Value));

      using (LastBackupsStatusContext updateTestContext = GetTemporalDBContext())
      {
        MongoDBLastBackupRepository repository = new MongoDBLastBackupRepository(updateTestContext);

        repository.Save(lastBackupStatusUpdated);
      }


      // Then Database has only 1 element and it has new data
      using (LastBackupsStatusContext thenContext = GetTemporalDBContext())
      {
        Assert.Equal(1, thenContext.LastBackupStatus.Count());
        LastBackupsStatusEntity backupInDB = thenContext.LastBackupStatus.First();
        LastBackupsStatusEntity backupUpdated = LastBackupsStatusEntity.FromDomain(lastBackupStatusUpdated);
        Assert.Equal(backupUpdated.Id, backupInDB.Id);
        Assert.NotNull(backupInDB.BackupTime);
        Assert.NotNull(backupUpdated.BackupTime);
        Assert.Equal(backupUpdated.BackupTime.Value, backupInDB.BackupTime.Value, TimeSpan.FromSeconds(0.9));

        DropDataBase();
      }
    }
  }
}