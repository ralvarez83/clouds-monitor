using System.Collections.Immutable;
using MediatR;
using QueryDomain = Shared.Domain.Bus.Query.Query;
using Shared.Infrastructure.Bus.Query.MediatR;
using SystemAdministrator.Machines.Application.GetAll;
using SystemAdministrator.Machines.Application.Dtos;

namespace SystemAdministrator.Machines.Infrastructure.Bus.Query.MediatR.GetAll
{
  public class MediatRGetAllQuery : GetAllQuery, IMediatRQuery<ImmutableList<MachineDto>, QueryDomain>
  {

    public static IRequest<ImmutableList<MachineDto>> Wrapper(QueryDomain request) => new MediatRGetAllQuery();
  }
}