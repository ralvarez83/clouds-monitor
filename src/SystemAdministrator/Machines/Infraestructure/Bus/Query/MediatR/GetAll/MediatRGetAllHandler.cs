using MediatR;
using System.Collections.Immutable;
using SystemAdministrator.Machines.Application.Dtos;
using SystemAdministrator.Machines.Application.GetAll;
using GetAllUseCase = SystemAdministrator.Machines.Application.GetAll.GetAll;

namespace SystemAdministrator.Machines.Infrastructure.Bus.Query.MediatR.GetAll
{
    public class MediatRGetCloudLastBackupsHandler(GetAllUseCase getAllUseCase) : GetAllHandler(getAllUseCase), IRequestHandler<MediatRGetAllQuery, ImmutableList<MachineDto>>
    {
        public async Task<ImmutableList<MachineDto>> Handle(MediatRGetAllQuery request, CancellationToken cancellationToken)
        {
            return await this.Handle((GetAllQuery)request, cancellationToken);
        }

    }
}