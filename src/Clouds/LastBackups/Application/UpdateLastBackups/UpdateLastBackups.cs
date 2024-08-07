using System.Collections.Immutable;
using Clouds.LastBackups.Application.Dtos;
using Clouds.LastBackups.Application.Dtos.Wrappers;
using Clouds.LastBackups.Application.GetCloudLast;
using Clouds.LastBackups.Domain;
using Clouds.LastBackups.Domain.ValueObjects;
using Shared.Domain.Bus.Event;
using Shared.Domain.Bus.Query;
using Shared.Domain.Criteria;
using Shared.Domain.Criteria.Filters;

namespace Clouds.LastBackups.Application.UpdateLastBackups
{
  public class UpdateLastBackups(BackupsRepository repository, QueryBus queryBus, EventBus eventBus)
  {
    private readonly BackupsRepository repository = repository;
    private readonly QueryBus queryBus = queryBus;
    private readonly EventBus eventBus = eventBus;

    public async Task Run()
    {
      ImmutableList<LastBackupStatus> lastBackups = (await queryBus.Ask<ImmutableList<LastBackupStatusDto>>(new GetCloudLastBackupsQuery())).AsParallel().Select(LastBackupStatusWrapper.FromDto).ToImmutableList();

      if (lastBackups.Count() > 0)
      {
        FilterValueList listMachineIds = new();
        listMachineIds.AddRange(lastBackups.AsParallel().Select(backup => backup.MachineId.Value).ToList<string>());
        Filter filterIds = new Filter(CloudMachineId.GetName(), listMachineIds.ToString(), FilterOperator.In);
        Filters filters = new Filters();
        filters.Add(filterIds);
        Criteria criteria = new Criteria(filters);

        ImmutableList<LastBackupStatus> backupsInRepository = await repository.Search(criteria);

        foreach (LastBackupStatus backupInRepository in backupsInRepository)
        {
          LastBackupStatus? cloudBackup = lastBackups.AsParallel().Where(backup => backup.MachineId.Value == backupInRepository.MachineId.Value).FirstOrDefault();
          if (null != cloudBackup && backupInRepository.Id.Value == cloudBackup.Id.Value)
          {
            lastBackups = lastBackups.AsParallel().Where(backup => backup.Id.Value != cloudBackup.Id.Value).ToImmutableList();
          }
        }

        lastBackups.ForEach(action: repository.Save);

        List<DomainEvent> events = new List<DomainEvent>();

        foreach (LastBackupStatus backupStatus in lastBackups)
        {
          events.AddRange(backupStatus.PullDomainEvents());
        }

        eventBus.Publish(events);
      }

    }
  }
}