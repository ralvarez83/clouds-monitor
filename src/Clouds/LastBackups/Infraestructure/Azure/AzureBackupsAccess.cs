using System.Collections.Immutable;
using Azure;
using Azure.Core;
using Azure.ResourceManager;
using Azure.ResourceManager.RecoveryServicesBackup;
using Azure.ResourceManager.Resources;
using Clouds.LastBackups.Infraestructure.Azure.Configuration;
using Clouds.LastBackups.Domain;
using Clouds.LastBackups.Infraestructure.Azure.Wrappers;

namespace Clouds.LastBackups.Infraestructure.Azure;

public class AzureBackupsAccess(ArmClient armClient, AzureEnvConfig azureEnvConfig) : BackupsCloudAccess
{
  private TenantsAccess _tenantsAccess = (TenantsAccess)azureEnvConfig;
  //private readonly ServiceBusSender _serviceBusSender = senderFactory.CreateClient("AzureServerMonitorQueue");
  private readonly ArmClient _client = armClient;
  public async Task<ImmutableList<Domain.LastBackupStatus>> GetLast()
  {
    ImmutableList<Domain.LastBackupStatus> lastBackups = [];

    foreach (Vaults vault in _tenantsAccess.Vaults)
    {
      ResourceIdentifier resourceGroupResourceId = ResourceGroupResource.CreateResourceIdentifier(vault.SuscriptionId, vault.ResourceGroupName);
      ResourceGroupResource resourceGroupResource = _client.GetResourceGroupResource(resourceGroupResourceId);
      Pageable<BackupProtectedItemResource> backupItems = resourceGroupResource.GetBackupProtectedItems(vault.Name);

      ImmutableList<Domain.LastBackupStatus?> lastVaultBackups = backupItems.Select(
        backupResourceItem => BackupProtectedItemDataWrapper.ToLastBackupStatus(backupResourceItem.Data, vault.SuscriptionId, _tenantsAccess.TenantId)
      ).ToImmutableList();

      lastBackups = [.. lastBackups, .. lastVaultBackups.Where(backup => null != backup)];
    }

    return await Task.Run(() => { return lastBackups; });
  }

  // public async Task<ImmutableList<LastBackups>> SearchByCriteria(Criteria criteria)
  // {
  //   AzureCriteriaTransformation criteriaTransformation = new AzureCriteriaTransformation(criteria);
  //   string filter = criteriaTransformation.GetCriterias();
  //   ImmutableList<LastBackups> backups = [];


  //   foreach (Vaults vault in _tenantsAccess.Vaults)
  //   {
  //     ResourceIdentifier resourceGroupResourceId = ResourceGroupResource.CreateResourceIdentifier(vault.SuscriptionIs, vault.ResourceGroupName);
  //     ResourceGroupResource resourceGroupResource = _client.GetResourceGroupResource(resourceGroupResourceId);
  //     BackupJobCollection collection = resourceGroupResource.GetBackupJobs(vault.Name);

  //     Pageable<BackupJobResource> backupJobs = collection.GetAll(filter: filter);
  //     ImmutableList<LastBackups> backupJobsDomain = backupJobs.Select(
  //         backup => new LastBackup(
  //           new BackupId(),
  //           new CloudBackupId(backup.Data.Id.ToString()),
  //           new BackupName(backup.Data.Properties.EntityFriendlyName),
  //           new LastBackupStatus(backup.Data.Properties.Status),
  //           backup.Data.Properties.StartOn.HasValue ? new BackupDate(backup.Data.Properties.StartOn.Value.DateTime) : null,
  //           backup.Data.Properties.EndOn.HasValue ? new BackupDate(backup.Data.Properties.EndOn.Value.DateTime) : null
  //         )
  //       ).ToImmutableList();

  //     backups = backups.Concat(backupJobsDomain).ToImmutableList();
  //   }

  //   return await Task.Run<ImmutableList<LastBackups>>(() => { return backups; });
  // }


}