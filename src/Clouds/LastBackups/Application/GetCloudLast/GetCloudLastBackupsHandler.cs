using System.Collections.Immutable;
using Clouds.LastBackups.Application.Dtos;
using Clouds.LastBackups.Application.Dtos.Transformation;
using Clouds.LastBackups.Domain;
using Shared.Domain.Bus.Command;
using Shared.Domain.Bus.Query;

namespace Clouds.LastBackups.Application.GetCloudLast
{
    public class GetCloudLastBackupsHandler(GetCloudLastBackups getCloudLastBackups) : QueryHandler<GetCloudLastBackupsQuery, ImmutableList<LastBackupStatusDto>>
    {
        private GetCloudLastBackups _getCloudLastBackups = getCloudLastBackups;
        public async Task<ImmutableList<LastBackupStatusDto>> Handle(GetCloudLastBackupsQuery query, CancellationToken cancellationToken)
        {
            ImmutableList<LastBackupStatus> lastBackupsInCloud = await _getCloudLastBackups.Run();

            return lastBackupsInCloud.Select(LastBackupStatusDtoWrapper.FromDomain).ToImmutableList();
        }
    }
}