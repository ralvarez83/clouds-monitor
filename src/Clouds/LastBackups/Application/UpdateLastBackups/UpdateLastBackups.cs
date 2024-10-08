using System.Collections.Immutable;
using Clouds.LastBackups.Application.Dtos;
using Clouds.LastBackups.Application.Dtos.Wrappers;
using Clouds.LastBackups.Application.GetCloudLastBackups;
using Clouds.LastBackups.Domain;
using Shared.Domain.Bus.Event;
using Shared.Domain.Bus.Query;
using Shared.Domain.Criteria;
using Shared.Domain.Criteria.Filters;
using Shared.Domain.ValueObjects;

namespace Clouds.LastBackups.Application.UpdateLastBackups
{
  public class UpdateLastBackups(LastBackupsRepository repository, QueryBus queryBus, EventBus eventBus)
  {
    private readonly LastBackupsRepository repository = repository;
    private readonly QueryBus queryBus = queryBus;
    private readonly EventBus eventBus = eventBus;

    public async Task Run()
    {
      ImmutableList<LastBackupStatus> lastBackups = (await queryBus.Ask<ImmutableList<LastBackupStatusDto>>(new GetCloudLastBackupsQuery())).AsParallel().Select(LastBackupStatusWrapper.FromDto).ToImmutableList();

      if (lastBackups.Count() > 0)
      {
        FilterValueList listMachineIds = new();
        listMachineIds.AddRange(lastBackups.AsParallel().Select(backup => backup.MachineId.Value).ToList<string>());
        Filter filterIds = new Filter(MachineId.GetName(), listMachineIds.ToString(), FilterOperator.In);
        Filters filters = new Filters();
        filters.Add(filterIds);
        Criteria criteria = new Criteria(filters);

        ImmutableList<LastBackupStatus> backupsInRepository = await repository.Search(criteria);
        List<string> filter = backupsInRepository.Select(backup => backup.MachineId.Value.ToString() + backup.BackupTime.ToString()).ToList();

        List<LastBackupStatus> lastBackupsToSave = lastBackups.Where(backup => !filter.Contains(backup.MachineId.Value.ToString() + backup.BackupTime.ToString())).ToList();

        lastBackupsToSave.ForEach(action: repository.Save);

        List<DomainEvent> events = new List<DomainEvent>();

        foreach (LastBackupStatus backupStatus in lastBackupsToSave)
        {
          events.AddRange(backupStatus.PullDomainEvents());
        }

        eventBus.Publish(events);
      }

    }
  }
}