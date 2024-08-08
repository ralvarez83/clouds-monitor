using System.Collections.Immutable;
using Clouds.LastBackups.Application.Dtos;
using Clouds.LastBackups.Application.Dtos.Transformation;
using Clouds.LastBackups.Application.Dtos.Wrappers;
using Clouds.LastBackups.Domain;
using Clouds.LastBackups.Infraestructure.Repository.EntityFramework;
using MongoDB.Driver.Linq;
using Shared.Infraestructure.Respository.EntityFramework.Criteria;
using CriteriaDomain = Shared.Domain.Criteria.Criteria;

namespace Clouds.LastBackups.Infraestructure.Repository.MongoDB
{
  public class MongoDBLastBackupRepository(LastBackupsStatusContext dbContext) : LastBackupsRepository
  {
    private readonly LastBackupsStatusContext dbContext = dbContext;

    public void Save(LastBackupStatus backup)
    {
      LastBackupStatusDto lastBackupStatusDto = LastBackupStatusDtoWrapper.FromDomain(backup);
      bool isInDB = dbContext.LastBackupStatus.Any(backup => backup.MachineId == lastBackupStatusDto.MachineId);

      if (isInDB)
      {
        dbContext.LastBackupStatus.Update(lastBackupStatusDto);
      }
      else
      {
        dbContext.Add(lastBackupStatusDto);
      }
      dbContext.SaveChanges();
    }

    public Task<ImmutableList<LastBackupStatus>> Search(CriteriaDomain criteria)
    {
      IQueryable<LastBackupStatusDto> backupsToReturn = dbContext.LastBackupStatus;
      if (criteria.filters.HasFilters())
      {
        ImmutableList<IFilter<LastBackupStatusDto>> filters = EntityFrameworkFiltersWrapper.FromDomainFilters(criteria.filters.FiltersFiled);
        foreach (var filter in filters)
        {
          backupsToReturn = backupsToReturn.Where(filter.ToExpression());
        }
      }
      return Task.Run(() => backupsToReturn.Select(LastBackupStatusWrapper.FromDto).ToImmutableList());
    }
  }
}