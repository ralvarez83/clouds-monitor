using Microsoft.AspNetCore.Mvc;
using SystemAdministrator.Machines.Application.Dtos;
using System.Collections.Immutable;
using Shared.Domain.Bus.Query;
using SystemAdministrator.Machines.Application.GetAll;

namespace WebAPI.Controllers.Machines.GetAll;


[ApiController]
[Route("/Machines/")]
public class MachinesGetAllController(QueryBus queryBus) : ControllerBase
{

    private QueryBus _queryBus = queryBus;

    [HttpGet("")]
    public async Task<ActionResult<ImmutableList<MachineDto>>> BackupsGetLast()
    {
        GetAllQuery query = new GetAllQuery();

        ImmutableList<MachineDto> backups = await _queryBus.Ask<ImmutableList<MachineDto>>(query);

        return backups;
    }
}