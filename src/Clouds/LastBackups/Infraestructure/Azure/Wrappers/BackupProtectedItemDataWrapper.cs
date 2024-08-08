
using Azure.ResourceManager.RecoveryServicesBackup;
using Azure.ResourceManager.RecoveryServicesBackup.Models;
using Clouds.LastBackups.Domain.ValueObjects;

namespace Clouds.LastBackups.Infraestructure.Azure.Wrappers
{
  public static class BackupProtectedItemDataWrapper
  {
    private static readonly Dictionary<string, Func<BackupGenericProtectedItem, string, string, Domain.LastBackupStatus?>> _wrappers = new()
    {
      {BackupDataSourceType.Vm.ToString(), FromVMMachine }
    };
    public static Domain.LastBackupStatus? ToLastBackupStatus(BackupProtectedItemData data, string backupSuscription, string backupTenant)
    {

      BackupGenericProtectedItem backup = data.Properties;

      Func<BackupGenericProtectedItem, string, string, Domain.LastBackupStatus?>? wrapper = _wrappers.GetValueOrDefault(backup.WorkloadType.ToString());

      if (null != wrapper)
      {
        return wrapper(backup, backupSuscription, backupTenant);
      }

      return null;
    }

    private static Domain.LastBackupStatus? FromVMMachine(BackupGenericProtectedItem item, string itemSuscriptionId, string itemTenantId)
    {
      IaasVmProtectedItem backupData = (IaasVmProtectedItem)item;
      BackupId backupId = new BackupId();
      CloudMachineId cloudMachineId = new CloudMachineId(backupData.VirtualMachineId);
      CloudMachineName cloudMachineName = new CloudMachineName(backupData.FriendlyName);
      BackupStatus backupStatus = BackupStatusWrapper.FromString(backupData.LastBackupStatus);
      BackupDate? backupTime = backupData.LastBackupOn.HasValue ? new BackupDate(backupData.LastBackupOn.Value) : null;
      BackupDate? lastRecovery = backupData.LastRecoverOn.HasValue ? new BackupDate(backupData.LastRecoverOn.Value) : null;
      Domain.ValueObjects.BackupType backupType = BackupTypeWrapper.FromSourceType(backupData.WorkloadType);
      VaultId vaultId = new VaultId(item.VaultId);
      SuscriptionId suscriptionId = new SuscriptionId(itemSuscriptionId);
      TenantId tenantId = new TenantId(itemTenantId);

      return new Domain.LastBackupStatus(
        backupId,
        cloudMachineId,
        cloudMachineName,
        backupStatus,
        backupTime,
        backupType,
        lastRecovery,
        vaultId,
        suscriptionId,
        tenantId
      );
    }
  }
}
// Data.Properties.FriendlyName
// Data.Properties.[Mas].VirtualMachineId
// Data.Properties.LastBackupStatus
// Data.Properties.LastBackupOn
// Data.Properties.LastRecoverOn
// Data.Properties.[Mas].WorkloadType