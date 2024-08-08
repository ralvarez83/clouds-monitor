using System.Collections.Immutable;
using Clouds.LastBackups.Domain;

namespace Clouds.LastBackups.Application.GetCloudLast
{
    public class GetCloudLastBackups(LastBackupsCloudAccess backupsCloudRepository)
    {
        private LastBackupsCloudAccess _backupsCloudRepository = backupsCloudRepository;

        public async Task<ImmutableList<LastBackupStatus>> Run()
        {
            ImmutableList<LastBackupStatus> lastBackups = await _backupsCloudRepository.GetLast();

            return lastBackups;
        }
    }
}