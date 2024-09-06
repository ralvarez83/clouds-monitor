using System.Collections.Immutable;
using SystemAdministrator.Machines.Domain;

namespace SystemAdministrator.Machines.Application.GetAll
{
  public class GetAll(MachinesRepository repository)
  {
    private readonly MachinesRepository repository = repository;

    public async Task<ImmutableList<Machine>> Run()
    {
      return await repository.GetAll();
    }
  }
}