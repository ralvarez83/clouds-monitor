using System.Collections.Immutable;
using Clouds.LastBackups.Domain;
using Clouds.LastBackups.Infrastructure.Repository.EntityFramework;
using MongoDB.Driver.Linq;
using Shared.Infrastructure.Respository.EntityFramework.Criteria;
using CriteriaDomain = Shared.Domain.Criteria.Criteria;

namespace Clouds.LastBackups.Infrastructure.Repository.MongoDB
{
  public class MongoDBLastBackupRepository(LastBackupsStatusContext dbContext) : LastBackupsRepository
  {
    private readonly LastBackupsStatusContext dbContext = dbContext;

    public void Save(LastBackupStatus backup)
    {
      LastBackupsStatusEntity? backupInDB = dbContext.LastBackupStatus.Where(backupInDB => backupInDB.Id == backup.MachineId.Value).FirstOrDefault();

      if (null != backupInDB)
      {
        backupInDB.BackupTime = null != backup.BackupTime ? backup.BackupTime.Value : null;
        backupInDB.LastRecoveryPoint = null != backup.LastRecoveryPoint ? backup.LastRecoveryPoint.Value : null;
        backupInDB.Status = backup.Status.ToString();

        dbContext.LastBackupStatus.Update(backupInDB);
      }
      else
      {
        dbContext.Add(LastBackupsStatusEntity.FromDomain(backup));
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


    public Task<ImmutableList<LastBackupStatus>> Search(CriteriaDomain criteria)
    {
      IQueryable<LastBackupsStatusEntity> backupsToReturn = dbContext.LastBackupStatus;
      if (criteria.filters.HasFilters())
      {
        ImmutableList<IFilter<LastBackupsStatusEntity>> filters = EntityFrameworkFiltersWrapper<LastBackupsStatusEntity>.FromDomainFilters(criteria.filters.FiltersFiled);
        foreach (var filter in filters)
        {
          backupsToReturn = backupsToReturn.Where(filter.ToExpression());
        }
      }
      return Task.Run(() => backupsToReturn.Select(LastBackupsStatusEntity.ToDomain).ToImmutableList());
    }
  }
}