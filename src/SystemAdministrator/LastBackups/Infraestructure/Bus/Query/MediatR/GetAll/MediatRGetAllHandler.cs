using MediatR;
using System.Collections.Immutable;
using SystemAdministrator.LastBackups.Application.Dtos;
using SystemAdministrator.LastBackups.Application.GetAll;
using GetAllUseCase = SystemAdministrator.LastBackups.Application.GetAll.GetAll;

namespace SystemAdministrator.LastBackups.Infrastructure.Bus.Query.MediatR.GetAll
{
    public class MediatRGetCloudLastBackupsHandler(GetAllUseCase getAllUseCase) : GetAllHandler(getAllUseCase), IRequestHandler<MediatRGetAllQuery, ImmutableList<MachineDto>>
    {
        public async Task<ImmutableList<MachineDto>> Handle(MediatRGetAllQuery request, CancellationToken cancellationToken)
        {
            return await this.Handle((GetAllQuery)request, cancellationToken);
        }

    }
}