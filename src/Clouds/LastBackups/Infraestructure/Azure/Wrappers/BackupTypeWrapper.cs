using Azure.ResourceManager.RecoveryServicesBackup.Models;
using BackupTypeDomain = Clouds.LastBackups.Domain.ValueObjects.BackupType;

namespace Clouds.LastBackups.Infraestructure.Azure.Wrappers
{
  public static class BackupTypeWrapper
  {
    private static readonly Dictionary<BackupDataSourceType, BackupTypeDomain> _backupWrapper = new Dictionary<BackupDataSourceType, BackupTypeDomain>()
    {
      {BackupDataSourceType.Vm, BackupTypeDomain.VirtualMachine}
    };
    public static BackupTypeDomain? FromSourceType(BackupDataSourceType? workLoad)
    {
      return workLoad.HasValue ? _backupWrapper.GetValueOrDefault(workLoad.Value) : null;
    }
  }
}