using MediatR;
using Clouds.LastBackups.Application.GetCloudLast;
using Clouds.LastBackups.Application.Dtos;
using System.Collections.Immutable;

namespace Clouds.LastBackups.Infraestructure.Bus.MediatR.GetCloudLast
{
    public class MediatRGetCloudLastBackupsHandler(GetCloudLastBackups cloudLastBackups) : GetCloudLastBackupsHandler(cloudLastBackups), IRequestHandler<MediatRGetCloudLastBackupsQuery, ImmutableList<LastBackupStatusDto>>
    {
        public async Task<ImmutableList<LastBackupStatusDto>> Handle(MediatRGetCloudLastBackupsQuery request, CancellationToken cancellationToken)
        {
            return await this.Handle((GetCloudLastBackupsQuery)request, cancellationToken);
        }

    }
}