using UpdateLastBackupsDomain = Clouds.LastBackups.Application.UpdateLastBackups.UpdateLastBackups;
using MediatR;
using Clouds.LastBackups.Application.UpdateLastBackups;

namespace Clouds.LastBackups.Infrastructure.Bus.Command.MediatR.UpdateLastBackups
{
    public class MediatRUpdateLastBackupsHandler(UpdateLastBackupsDomain updateLastBackups) : UpdateLastBackupsHandler(updateLastBackups), IRequestHandler<MediatRUpdateLastBackupsCommand>
    {

        public async Task Handle(MediatRUpdateLastBackupsCommand request, CancellationToken cancellationToken)
        {
            await this.Handle(request);
        }
    }
}