using System.Collections.Immutable;
using Shared.Domain.Criteria;
using Shared.Domain.Criteria.Filters;
using SystemAdministrator.LastBackups.Domain;
using SystemAdministrator.LastBackups.Domain.ValueObjects;

namespace SystemAdministrator.LastBackups.Application.BackupsGetLastNDays;

public class BackupsGetAllLastNDays(LastBackupsRepository repository)
{
  private LastBackupsRepository _Repository { get; set; } = repository;

  public async Task<ImmutableList<Backup>> Search(int days)
  {

    DateTime endTime = DateTime.Today.AddDays(1);
    DateTime startTime = endTime.AddDays(days * -1);

    Filters filters = new Filters();
    filters.Add(new Filter(BackupDate.GetName(), endTime.ToString(), FilterOperator.LessEqualThan));
    filters.Add(new Filter(BackupDate.GetName(), startTime.ToString(), FilterOperator.GreaterEqualThan));
    Criteria criteria = new Criteria(filters);

    ImmutableList<Backup> backups = await _Repository.SearchByCriteria(criteria);

    return backups;
  }

}
