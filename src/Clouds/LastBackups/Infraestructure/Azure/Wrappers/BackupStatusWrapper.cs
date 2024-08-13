using BackupStatusDomain = Shared.Domain.ValueObjects.BackupStatus;

namespace Clouds.LastBackups.Infraestructure.Azure.Wrappers
{
  public static class BackupStatusWrapper
  {
    private static readonly Dictionary<string, BackupStatusDomain> _backupWrapper = new Dictionary<string, BackupStatusDomain>()
    {
      {"Completed", BackupStatusDomain.Completed},
      {"Incompleted", BackupStatusDomain.Incompleted}
    };
    public static BackupStatusDomain? FromString(string status)
    {
      return _backupWrapper.GetValueOrDefault(status);
    }
  }
}