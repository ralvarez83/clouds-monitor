using System.Collections.Immutable;
using SystemAdministrationTest.Backups.Domain;
using SystemAdministrator.LastBackups.Domain;
using SystemAdministrator.LastBackups.Infrastructure.Repository.MongoDB;

namespace SystemAdministrationTest.LastBackups.Infrastructure.MongoDB
{
  public class GetAll : CloudMongoDBUnitCase
  {
    [Fact]
    public async Task Recovery_AllMachinesAsync()
    {
      // Given the Database has Machines
      ImmutableList<Machine> machinesInRepository = MachineFactory.BuildArrayOfBackupsRandom();

      using (BackupsContext existingDataContext = GetTemporalDBContext())
      {
        existingDataContext.Backups.AddRange(machinesInRepository.Select(BackupsEntity.FromDomain).ToArray());
        existingDataContext.SaveChanges();
      }

      // When retrieve all machines
      ImmutableList<Machine>? machinesReturned = null;
      using (BackupsContext whenBackupContext = GetTemporalDBContext())
      {
        MongoDBBackupsRepository repository = new MongoDBBackupsRepository(whenBackupContext);
        machinesReturned = await repository.GetAll();
      }

      // Then returned
      Assert.Equal(machinesInRepository.Count, machinesReturned.Count);
      foreach (Machine machineReturned in machinesReturned)
      {
        Machine machineInRepository = machinesInRepository.First(machine => machine.MachineId.Value.Equals(machineReturned.MachineId.Value));
        Assert.NotNull(machineInRepository);
        Assert.Equal(machineInRepository.MachineId.Value, machineReturned.MachineId.Value);
        Assert.NotNull(machineInRepository.LastBackupTime);
        Assert.NotNull(machineReturned.LastBackupTime);
        Assert.Equal(machineInRepository.LastBackupTime.Value, machineReturned.LastBackupTime.Value, TimeSpan.FromSeconds(0.9));
      }

      using (BackupsContext thenContext = GetTemporalDBContext())
      {
        DropDataBase();
      }
    }
  }
}