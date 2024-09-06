using System.Collections.Immutable;

namespace Clouds.LastBackups.Domain;

public interface LastBackupsCloudAccess
{
  public Task<ImmutableList<LastBackupStatus>> GetLast();

}