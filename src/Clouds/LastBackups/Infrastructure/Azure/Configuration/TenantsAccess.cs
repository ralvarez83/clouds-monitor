using Microsoft.Extensions.Options;

namespace Clouds.LastBackups.Infrastructure.Azure.Configuration;

public class TenantsAccess : AzureEnvConfig
{
  public List<Vaults> Vaults { get; init; }
  public string TenantId { get; init; }
  public string ClientId { get; init; }
  public string ClientSecret { get; init; }

  public TenantsAccess(IOptions<List<Suscriptions>> options)
  {
    string? TenantId = Environment.GetEnvironmentVariable("AZURE_TENANT_ID");
    string? ClientId = Environment.GetEnvironmentVariable("AZURE_CLIENT_ID");
    string? ClientSecret = Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET");

    if (null == TenantId || null == ClientId || null == ClientSecret)
      throw new Exception("Enviroment variables not found");

    List<Suscriptions> suscriptions = options.Value;
    this.Vaults = (from suscription in suscriptions
                   from resourceGroup in suscription.ResourcesGroups
                   from vault in resourceGroup.Vaults
                   select new Vaults()
                   {
                     Name = vault.Name,
                     ResourceGroupName = resourceGroup.Name,
                     SuscriptionId = suscription.Id
                   }
                  ).ToList();

    //this.SubscriptionsIds = SubscriptionSIds.Split(SUBSCRIPTON_SEPARATOR);
    //this.Location = Location;
    this.TenantId = TenantId;
    this.ClientId = ClientId;
    this.ClientSecret = ClientSecret;
    //this.ResourceGroupName = ResourceGroupName;
    //this.VaultName = VaultName;

  }
  // public string[] SubscriptionsIds {get; init;}
  // public string Location {get; init;}
  // public string ResourceGroupName {get; init;}
  // public string VaultName {get; init;}

  public static string Name
  {
    get
    {
      return "TenantsAccess";
    }
  }
}
