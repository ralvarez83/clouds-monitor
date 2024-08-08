using System.Collections.Immutable;
using Shared.Domain.Criteria;

namespace Clouds.LastBackups.Domain;

public interface LastBackupsCloudAccess
{
  // public Task<ImmutableList<LastBackup>> SearchByCriteria(Criteria criteria);

  public Task<ImmutableList<LastBackupStatus>> GetLast();

}