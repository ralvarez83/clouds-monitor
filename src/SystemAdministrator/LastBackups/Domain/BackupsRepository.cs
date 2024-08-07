using System.Collections.Immutable;
using Shared.Domain.Criteria;

namespace SystemAdministrator.LastBackups.Domain;

public interface BackupsRepository
{
  public void Save(Backup backup);
  public Task<ImmutableList<Backup>> SearchByCriteria(Criteria criteria);

}