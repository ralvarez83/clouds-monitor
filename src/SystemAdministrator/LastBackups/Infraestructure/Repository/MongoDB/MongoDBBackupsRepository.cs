using System.Collections.Immutable;
using Shared.Domain.Criteria;
using Shared.Domain.ValueObjects;
using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrator.LastBackups.Infrastructure.Repository.MongoDB
{
  public class MongoDBBackupsRepository(BackupsContext backupContext) : BackupsRepository
  {
    private BackupsContext dbContext = backupContext;

    public async Task<Backup?> GetById(MachineId id)
    {
      BackupsEntity? entity = await dbContext.Backups.FindAsync(id.Value);
      if (null == entity)
        return null;
      return BackupsEntity.ToDomain(entity);
    }

    public void Save(Backup backup)
    {
      BackupsEntity? backupInDB = dbContext.Backups.Where(backupInDB => backupInDB.Id == backup.MachineId.Value).FirstOrDefault();

      if (null != backupInDB)
      {
        backupInDB.BackupTime = null != backup.BackupTime ? backup.BackupTime.Value : null;
        backupInDB.LastRecoveryPoint = null != backup.LastRecoveryPoint ? backup.LastRecoveryPoint.Value : null;
        backupInDB.Status = backup.Status.ToString();

        dbContext.Backups.Update(backupInDB);
      }
      else
      {
        dbContext.Add(BackupsEntity.FromDomain(backup));
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

    public Task<ImmutableList<Backup>> Search(Criteria criteria)
    {
      throw new NotImplementedException();
    }
  }
}