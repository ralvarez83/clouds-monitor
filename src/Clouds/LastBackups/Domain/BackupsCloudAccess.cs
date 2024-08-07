using System.Collections.Immutable;
using Shared.Domain.Criteria;

namespace Clouds.LastBackups.Domain;

public interface BackupsCloudAccess
{
  // public Task<ImmutableList<LastBackup>> SearchByCriteria(Criteria criteria);

  public Task<ImmutableList<LastBackupStatus>> GetLast();

}