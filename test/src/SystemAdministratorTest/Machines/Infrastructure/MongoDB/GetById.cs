using System.Collections.Immutable;
using SystemAdministrationTest.Machines.Domain;
using SystemAdministrator.Machines.Domain;
using SystemAdministrator.Machines.Infrastructure.Repository.MongoDB;

namespace SystemAdministrationTest.Machines.Infrastructure.MongoDB
{
  public class GetById : CloudMongoDBUnitCase
  {
    [Fact]
    public async void Get_ExistingBackup()
    {
      // Given database has backups
      ImmutableList<Machine> backups = MachineFactory.BuildArrayOfBackupsRandom();
      Machine backupToFind = backups.First();

      using (MachinesContext existingDataContext = GetTemporalDBContext())
      {
        existingDataContext.Backups.AddRange(backups.Select(MachinesEntity.FromDomain).ToArray());
        existingDataContext.SaveChanges();
      }

      // When look for a existing Machine Id
      Machine? backupFinded;
      using (MachinesContext whenBackupContext = GetTemporalDBContext())
      {
        MongoDBMachinesRepository repository = new MongoDBMachinesRepository(whenBackupContext);
        backupFinded = await repository.GetById(backupToFind.MachineId);
      }

      // Then get the the Machine last backup information
      using (MachinesContext thenContext = GetTemporalDBContext())
      {
        Assert.NotNull(backupFinded);
        Assert.Equal(backupFinded.MachineId.Value, backupToFind.MachineId.Value);
        Assert.NotNull(backupFinded.LastBackupTime);
        Assert.NotNull(backupToFind.LastBackupTime);
        Assert.Equal(backupFinded.LastBackupTime.Value, backupToFind.LastBackupTime.Value, TimeSpan.FromSeconds(0.9));

        DropDataBase();
      }
    }

    [Fact]
    public async void ID_NotFound_ShouldReturnNull()
    {
      // Given the Database has Backups
      ImmutableList<Machine> backups = MachineFactory.BuildArrayOfBackupsRandom();

      using (MachinesContext existingDataContext = GetTemporalDBContext())
      {
        existingDataContext.Backups.AddRange(backups.Select(MachinesEntity.FromDomain).ToArray());
        existingDataContext.SaveChanges();
      }
      // When the finding Id is not in the Database
      Machine backupToFind = MachineFactory.BuildBackupRandom();
      Machine? backupFinded;
      using (MachinesContext whenBackupContext = GetTemporalDBContext())
      {
        MongoDBMachinesRepository repository = new MongoDBMachinesRepository(whenBackupContext);
        backupFinded = await repository.GetById(backupToFind.MachineId);
      }

      // Then return null
      using (MachinesContext thenContext = GetTemporalDBContext())
      {
        Assert.Null(backupFinded);
        DropDataBase();
      }
    }
  }
}