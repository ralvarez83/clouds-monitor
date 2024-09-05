using System.Collections.Immutable;
using Shared.Domain.Bus.Query;
using SystemAdministrator.LastBackups.Application.Dtos;
using SystemAdministrator.LastBackups.Application.Dtos.Wrappers;

namespace SystemAdministrator.LastBackups.Application.GetAll
{
  public class GetAllHandler(GetAll getAll) : QueryHandler<GetAllQuery, ImmutableList<MachineDto>>
  {
    private readonly GetAll getAll = getAll;

    public async Task<ImmutableList<MachineDto>> Handle(GetAllQuery query, CancellationToken cancellationToken)
    {
      return (await getAll.Run()).Select(MachineDtoWrapper.FromDomain).ToImmutableList();
    }
  }
}