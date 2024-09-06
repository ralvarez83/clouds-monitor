using Shared.Domain.ValueObjects;
using SystemAdministrationTest.Machines.Domain;
using SystemAdministrator.Machines.Domain;
using SystemAdministrator.Machines.Infrastructure.Repository.MongoDB;

namespace SystemAdministrationTest.Machines.Infrastructure.MongoDB
{
  public class SaveBackup : CloudMongoDBUnitCase
  {

    [Fact]
    public void Add_new_Backup()
    {
      // Given database is empty and I have a new Backup
      Machine Backup = MachineFactory.BuildBackupRandom();

      // When save the new element
      using (MachinesContext testContext = GetTemporalDBContext())
      {
        MongoDBMachinesRepository repository = new MongoDBMachinesRepository(testContext);
        repository.Save(Backup);
      }

      // Then the element is in the daba base
      using (MachinesContext thenContext = GetTemporalDBContext())
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
      using (MachinesContext existingDataContext = GetTemporalDBContext())
      {
        existingDataContext.Backups.Add(MachinesEntity.FromDomain(Backup));
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

      using (MachinesContext updateTestContext = GetTemporalDBContext())
      {
        MongoDBMachinesRepository repository = new MongoDBMachinesRepository(updateTestContext);

        repository.Save(BackupUpdated);
      }


      // Then Database has only 1 element and it has new data
      using (MachinesContext thenContext = GetTemporalDBContext())
      {
        Assert.Equal(1, thenContext.Backups.Count());
        MachinesEntity backupInDB = thenContext.Backups.First();
        MachinesEntity backupUpdated = MachinesEntity.FromDomain(BackupUpdated);
        Assert.Equal(backupUpdated.Id, backupInDB.Id);
        Assert.NotNull(backupInDB.LastBackupTime);
        Assert.NotNull(backupUpdated.LastBackupTime);
        Assert.Equal(backupUpdated.LastBackupTime.Value, backupInDB.LastBackupTime.Value, TimeSpan.FromSeconds(0.9));

        DropDataBase();
      }
    }
  }
}