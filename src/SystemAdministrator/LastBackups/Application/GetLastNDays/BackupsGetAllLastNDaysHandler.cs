using System.Collections.Immutable;
using SystemAdministrator.LastBackups.Application.Dtos;
using SystemAdministrator.LastBackups.Application.Dtos.Transformation;
using SystemAdministrator.LastBackups.Domain;
using Shared.Domain.Bus.Query;

namespace SystemAdministrator.LastBackups.Application.BackupsGetLastNDays
{
    public class BackupsGetAllLastNDaysHandler(BackupsGetAllLastNDays backupsGetAllLastNDays) : QueryHandler<BackupsGetAllLastNDaysQuery, ImmutableList<BackupDto>>
    {
        private BackupsGetAllLastNDays _backupsGetAllLastNDays = backupsGetAllLastNDays;
        public async Task<ImmutableList<BackupDto>> Handle(BackupsGetAllLastNDaysQuery query, CancellationToken cancellationToken)
        {
            ImmutableList<Backup> backups = await _backupsGetAllLastNDays.Search(query.Days);

            ImmutableList<BackupDto> backupDtos = backups.Select(BackupToBackupDTOTransformation.Run).ToImmutableList();

            return backupDtos;
        }
    }
}