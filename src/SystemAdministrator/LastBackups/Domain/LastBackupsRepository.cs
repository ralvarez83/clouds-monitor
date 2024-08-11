using System.Collections.Immutable;
using Shared.Domain.Criteria;
using Shared.Domain.ValueObjects;

namespace SystemAdministrator.LastBackups.Domain;

public interface LastBackupsRepository
{
  public void Save(Backup backup);

  public Task<Backup?> GetById(MachineId id);
  public Task<ImmutableList<Backup>> SearchByCriteria(Criteria criteria);

}