using System.Collections.Immutable;
using SystemAdministrator.LastBackups.Application.Dtos;
using SystemAdministrator.LastBackups.Application.Dtos.Transformation;
using Shared.Domain.Bus.Query;
using SystemAdministrator.LastBackups.Domain;
using SystemAdministrator.LastBackups.Domain.ValueObjects;
using Shared.Backups.Application.BackupsGetLastNDays;

namespace SystemAdministrator.LastBackups.Application.BackupsGetLastNDays
{
    public class BackupsGetByCloudIdHandler(BackupsGetByCloudId backupsGetByCloudId) : QueryHandler<BackupsGetByCloudIdQuery, BackupDto?>
    {
        private BackupsGetByCloudId _backupsGetByCloudId = backupsGetByCloudId;
        public async Task<BackupDto?> Handle(BackupsGetByCloudIdQuery query, CancellationToken cancellationToken)
        {
            ImmutableList<Backup> backups = await _backupsGetByCloudId.Search(new CloudBackupId(query.CloudBackupId));

            BackupDto? backupDtos = backups.Select(BackupToBackupDTOTransformation.Run).FirstOrDefault();

            return backupDtos;
        }
    }
}