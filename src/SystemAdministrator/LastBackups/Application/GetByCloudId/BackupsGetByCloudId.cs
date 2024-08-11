using System.Collections.Immutable;
using SystemAdministrator.LastBackups.Domain;
using SystemAdministrator.LastBackups.Domain.ValueObjects;
using Shared.Domain.Criteria;
using Shared.Domain.Criteria.Filters;

namespace Shared.Backups.Application.BackupsGetLastNDays;

public class BackupsGetByCloudId(LastBackupsRepository repository)
{
  private LastBackupsRepository _Repository { get; set; } = repository;

  public async Task<ImmutableList<Backup>> Search(CloudBackupId cloudBackupId)
  {

    Filters filters = new Filters();
    filters.Add(new Filter(Filter.CLOUD_ID, cloudBackupId.Value, FilterOperator.Equal));
    Criteria criteria = new Criteria(filters);

    ImmutableList<Backup> backups = await _Repository.SearchByCriteria(criteria);

    return backups;
  }

}
