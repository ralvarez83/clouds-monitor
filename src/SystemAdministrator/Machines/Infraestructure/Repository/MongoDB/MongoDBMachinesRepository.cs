using System.Collections.Immutable;
using Shared.Domain.Criteria;
using Shared.Domain.ValueObjects;
using SystemAdministrator.Machines.Domain;

namespace SystemAdministrator.Machines.Infrastructure.Repository.MongoDB
{
  public class MongoDBMachinesRepository(MachinesContext backupContext) : MachinesRepository
  {
    private MachinesContext dbContext = backupContext;

    public Task<ImmutableList<Machine>> GetAll()
    {
      return Task.Run(() => dbContext.Backups.Select(MachinesEntity.ToDomain).ToImmutableList());
    }

    public async Task<Machine?> GetById(MachineId id)
    {
      MachinesEntity? entity = await dbContext.Backups.FindAsync(id.Value);
      if (null == entity)
        return null;
      return MachinesEntity.ToDomain(entity);
    }

    public void Save(Machine backup)
    {
      MachinesEntity? backupInDB = dbContext.Backups.Where(backupInDB => backupInDB.Id == backup.MachineId.Value).FirstOrDefault();

      if (null != backupInDB)
      {
        backupInDB.LastBackupTime = null != backup.LastBackupTime ? backup.LastBackupTime.Value : null;
        backupInDB.LastRecoveryPoint = null != backup.LastRecoveryPoint ? backup.LastRecoveryPoint.Value : null;
        backupInDB.LastBackupStatus = backup.LastBackupStatus.ToString();

        dbContext.Backups.Update(backupInDB);
      }
      else
      {
        dbContext.Add(MachinesEntity.FromDomain(backup));
      }
      try
      {
        dbContext.SaveChanges();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }
    }

    public Task<ImmutableList<Machine>> Search(Criteria criteria)
    {
      throw new NotImplementedException();
    }
  }
}