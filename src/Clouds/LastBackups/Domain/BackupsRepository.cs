using System.Collections.Immutable;
using Shared.Domain.Criteria;

namespace Clouds.LastBackups.Domain;

public interface LastBackupsRepository
{
  public void Save(LastBackupStatus backup);
  // public Task<ImmutableList<LastBackups>> SearchByCriteria(Criteria criteria);

  public Task<ImmutableList<LastBackupStatus>> Search(Criteria criteria);

}