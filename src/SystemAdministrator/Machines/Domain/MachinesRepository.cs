using System.Collections.Immutable;
using Shared.Domain.Criteria;
using Shared.Domain.ValueObjects;

namespace SystemAdministrator.Machines.Domain;

public interface MachinesRepository
{
  public void Save(Machine backup);

  public Task<Machine?> GetById(MachineId id);
  public Task<ImmutableList<Machine>> GetAll();
  public Task<ImmutableList<Machine>> Search(Criteria criteria);

}