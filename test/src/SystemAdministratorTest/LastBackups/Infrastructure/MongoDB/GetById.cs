using System.Collections.Immutable;
using SystemAdministrationTest.Backups.Domain;
using SystemAdministrator.LastBackups.Domain;
using SystemAdministrator.LastBackups.Infrastructure.Repository.MongoDB;

namespace SystemAdministrationTest.LastBackups.Infrastructure.MongoDB
{
  public class GetById : CloudMongoDBUnitCase
  {
    [Fact]
    public async void Get_ExistingBackup()
    {
      // Given database has backups
      ImmutableList<Backup> backups = BackupsFactory.BuildArrayOfBackupsRandom();
      Backup backupToFind = backups.First();

      using (BackupsContext existingDataContext = GetTemporalDBContext())
      {
        existingDataContext.Backups.AddRange(backups.Select(BackupsEntity.FromDomain).ToArray());
        existingDataContext.SaveChanges();
      }

      // When look for a existing Machine Id
      Backup? backupFinded;
      using (BackupsContext whenBackupContext = GetTemporalDBContext())
      {
        MongoDBBackupsRepository repository = new MongoDBBackupsRepository(whenBackupContext);
        backupFinded = await repository.GetById(backupToFind.MachineId);
      }

      // Then get the the Machine last backup information
      using (BackupsContext thenContext = GetTemporalDBContext())
      {
        Assert.NotNull(backupFinded);
        Assert.Equal(backupFinded.MachineId.Value, backupToFind.MachineId.Value);
        Assert.NotNull(backupFinded.BackupTime);
        Assert.NotNull(backupToFind.BackupTime);
        Assert.Equal(backupFinded.BackupTime.Value, backupToFind.BackupTime.Value, TimeSpan.FromSeconds(0.9));

        DropDataBase();
      }
    }

    [Fact]
    public async void ID_NotFound_ShouldReturnNull()
    {
      // Given the Database has Backups
      ImmutableList<Backup> backups = BackupsFactory.BuildArrayOfBackupsRandom();

      using (BackupsContext existingDataContext = GetTemporalDBContext())
      {
        existingDataContext.Backups.AddRange(backups.Select(BackupsEntity.FromDomain).ToArray());
        existingDataContext.SaveChanges();
      }
      // When the finding Id is not in the Database
      Backup backupToFind = BackupsFactory.BuildBackupRandom();
      Backup? backupFinded;
      using (BackupsContext whenBackupContext = GetTemporalDBContext())
      {
        MongoDBBackupsRepository repository = new MongoDBBackupsRepository(whenBackupContext);
        backupFinded = await repository.GetById(backupToFind.MachineId);
      }

      // Then return null
      using (BackupsContext thenContext = GetTemporalDBContext())
      {
        Assert.Null(backupFinded);
        DropDataBase();
      }
    }
  }
}