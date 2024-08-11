using System.Collections.Immutable;
using SystemAdministrator.LastBackups.Application.BackupsGetLastNDays;
using SystemAdministrator.LastBackups.Application.Dtos;
using SystemAdministrator.LastBackups.Application.Dtos.Transformation;
using Shared.Domain.Bus.Query;
using SystemAdministrator.LastBackups.Domain;

namespace SystemAdministrator.LastBackups.Application.BackupsGetLast;

public class BackupsGetLast(QueryBus queryBus)
{
    private QueryBus _queryBus = queryBus;
    const int DAYS_BACKUP_RECOVERY = 2;

    public async Task<ImmutableList<Backup>> Search()
    {
        ImmutableList<Backup> allBackups = await GetLastBackups();

        IEnumerable<Backup> lastBackups = from backup in allBackups
                                          group backup by backup.name.Value into serverGroup
                                          select serverGroup.OrderByDescending(t => (null != t.startTime) ? t.startTime.Value : DateTime.MinValue).FirstOrDefault();


        return lastBackups.ToImmutableList();
    }

    private async Task<ImmutableList<Backup>> GetLastBackups()
    {
        BackupsGetAllLastNDaysQuery queryLast = new BackupsGetAllLastNDaysQuery(DAYS_BACKUP_RECOVERY);

        ImmutableList<BackupDto> allBackupsDto = await _queryBus.Ask<ImmutableList<BackupDto>>(queryLast);
        ImmutableList<Backup> allBackups = allBackupsDto.Select(BackupDtoToBackupTransformation.Run).ToImmutableList();
        return allBackups;
    }
}