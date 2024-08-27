using Shared.Domain.ValueObjects;
using SystemAdministrationTest.Backups.Domain;
using SystemAdministrator.LastBackups.Domain;
using SystemAdministrator.LastBackups.Infrastructure.Repository.MongoDB;

namespace SystemAdministrationTest.LastBackups.Infrastructure.MongoDB
{
  public class SaveBackup : CloudMongoDBUnitCase
  {

    [Fact]
    public void Add_new_Backup()
    {
      // Given database is empty and I have a new Backup
      Machine Backup = MachineFactory.BuildBackupRandom();

      // When save the new element
      using (BackupsContext testContext = GetTemporalDBContext())
      {
        MongoDBBackupsRepository repository = new MongoDBBackupsRepository(testContext);
        repository.Save(Backup);
      }

      // Then the element is in the daba base
      using (BackupsContext thenContext = GetTemporalDBContext())
      {
        Assert.NotEmpty(thenContext.Backups);
        DropDataBase();
      }
    }

    [Fact]
    public void Update_existing_Backup()
    {
      // Given database has one Backup
      Machine Backup = MachineFactory.BuildBackupRandom();
      using (BackupsContext existingDataContext = GetTemporalDBContext())
      {
        existingDataContext.Backups.Add(BackupsEntity.FromDomain(Backup));
        existingDataContext.SaveChanges();
      }

      // When a field change and we save the element
      BackupDate newBackupDate = new BackupDate(DateTime.Now);
      Machine BackupUpdated = new(new MachineId(Backup.MachineId.Value),
                                                    new MachineName(Backup.MachineName.Value),
                                                    BackupStatus.Parse(Backup.LastBackupStatus.ToString()),
                                                    newBackupDate,
                                                    BackupType.Parse(Backup.LastBackupType.ToString()),
                                                    null != Backup.LastRecoveryPoint ? new BackupDate(Backup.LastRecoveryPoint.Value) : null,
                                                    new VaultId(Backup.VaultId.Value),
                                                    new SuscriptionId(Backup.SuscriptionId.Value),
                                                    new TenantId(Backup.TenantId.Value));

      using (BackupsContext updateTestContext = GetTemporalDBContext())
      {
        MongoDBBackupsRepository repository = new MongoDBBackupsRepository(updateTestContext);

        repository.Save(BackupUpdated);
      }


      // Then Database has only 1 element and it has new data
      using (BackupsContext thenContext = GetTemporalDBContext())
      {
        Assert.Equal(1, thenContext.Backups.Count());
        BackupsEntity backupInDB = thenContext.Backups.First();
        BackupsEntity backupUpdated = BackupsEntity.FromDomain(BackupUpdated);
        Assert.Equal(backupUpdated.Id, backupInDB.Id);
        Assert.NotNull(backupInDB.BackupTime);
        Assert.NotNull(backupUpdated.BackupTime);
        Assert.Equal(backupUpdated.BackupTime.Value, backupInDB.BackupTime.Value, TimeSpan.FromSeconds(0.9));

        DropDataBase();
      }
    }
  }
}