using System.Collections.Immutable;
using SystemAdministrationTest.Machines.Domain;
using SystemAdministrator.Machines.Domain;
using SystemAdministrator.Machines.Infrastructure.Repository.MongoDB;

namespace SystemAdministrationTest.Machines.Infrastructure.MongoDB
{
  public class GetAll : CloudMongoDBUnitCase
  {
    [Fact]
    public async Task Recovery_AllMachinesAsync()
    {
      // Given the Database has Machines
      ImmutableList<Machine> machinesInRepository = MachineFactory.BuildArrayOfBackupsRandom();

      using (MachinesContext existingDataContext = GetTemporalDBContext())
      {
        existingDataContext.Backups.AddRange(machinesInRepository.Select(MachinesEntity.FromDomain).ToArray());
        existingDataContext.SaveChanges();
      }

      // When retrieve all machines
      ImmutableList<Machine>? machinesReturned = null;
      using (MachinesContext whenBackupContext = GetTemporalDBContext())
      {
        MongoDBMachinesRepository repository = new MongoDBMachinesRepository(whenBackupContext);
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

      using (MachinesContext thenContext = GetTemporalDBContext())
      {
        DropDataBase();
      }
    }
  }
}