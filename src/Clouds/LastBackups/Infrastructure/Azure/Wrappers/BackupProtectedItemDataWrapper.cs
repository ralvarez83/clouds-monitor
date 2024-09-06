
using Azure.ResourceManager.RecoveryServicesBackup;
using Azure.ResourceManager.RecoveryServicesBackup.Models;
using Shared.Domain.ValueObjects;
using BackupTypeType = Shared.Domain.ValueObjects.BackupType;

namespace Clouds.LastBackups.Infrastructure.Azure.Wrappers
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
      MachineId cloudMachineId = new MachineId(backupData.VirtualMachineId);
      MachineName cloudMachineName = new MachineName(backupData.FriendlyName);
      BackupStatus backupStatus = BackupStatusWrapper.FromString(backupData.LastBackupStatus);
      BackupDate? backupTime = backupData.LastBackupOn.HasValue ? new BackupDate(backupData.LastBackupOn.Value) : null;
      BackupDate? lastRecovery = backupData.LastRecoverOn.HasValue ? new BackupDate(backupData.LastRecoverOn.Value) : null;
      BackupTypeType backupType = BackupTypeWrapper.FromSourceType(backupData.WorkloadType);
      VaultId vaultId = new VaultId(item.VaultId);
      SuscriptionId suscriptionId = new SuscriptionId(itemSuscriptionId);
      TenantId tenantId = new TenantId(itemTenantId);

      return new Domain.LastBackupStatus(
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