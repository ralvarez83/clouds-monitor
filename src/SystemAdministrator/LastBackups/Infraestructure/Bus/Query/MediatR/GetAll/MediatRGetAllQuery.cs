using System.Collections.Immutable;
using MediatR;
using QueryDomain = Shared.Domain.Bus.Query.Query;
using Shared.Infrastructure.Bus.Query.MediatR;
using SystemAdministrator.LastBackups.Application.GetAll;
using SystemAdministrator.LastBackups.Application.Dtos;

namespace SystemAdministrator.LastBackups.Infrastructure.Bus.Query.MediatR.GetAll
{
  public class MediatRGetAllQuery : GetAllQuery, IMediatRQuery<ImmutableList<MachineDto>, QueryDomain>
  {

    public static IRequest<ImmutableList<MachineDto>> Wrapper(QueryDomain request) => new MediatRGetAllQuery();
  }
}