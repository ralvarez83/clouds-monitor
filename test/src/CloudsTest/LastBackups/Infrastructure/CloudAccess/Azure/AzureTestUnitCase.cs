using System.Collections.Immutable;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Clouds.LastBackups.Domain;
using Clouds.LastBackups.Infraestructure.Azure;
using Clouds.LastBackups.Infraestructure.Azure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Enviroment;
using SharedTest.Infrastructure;

namespace CloudsTest.LastBackups.Infrastructure.CloudAccess.Azure
{
  public class AzureTestUnitCase : InfrastructureTestCase, IDisposable
  {
    protected List<Suscriptions>? _suscriptionsConfiguration;
    protected AzureBackupsAccess _azureAccess;

    protected ImmutableList<LastBackupStatus> _backupsReturn;

    public AzureTestUnitCase() : base()
    {
      LoadEnvironmentVariables? azureEnvironmentVariables = GetService<LoadEnvironmentVariables>();
      if (null == azureEnvironmentVariables)
        throw new Exception("Service LoadEnvironmentVariables doesn't found.");

      azureEnvironmentVariables.RegisterVariables();

      TenantsAccess? tenantsAccess = (TenantsAccess?)GetService<AzureEnvConfig>();
      if (null == tenantsAccess)
        throw new Exception("Service TenantsAccess doesn't found.");

      TokenCredential cred = new DefaultAzureCredential();

      // authenticate your client
      ArmClient client = new ArmClient(cred);

      _azureAccess = new AzureBackupsAccess(client, tenantsAccess);
      _backupsReturn = new List<LastBackupStatus>().ToImmutableList();
    }

    public void Dispose() => Finish();

    protected override Action<IServiceCollection> GetServices()
    {
      return services =>
      {
        var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

        services.Configure<List<Suscriptions>>(configuration.GetSection(Suscriptions.Name));
        services.Configure<List<EnvironmentVariables>>(configuration.GetSection(EnvironmentVariables.Name));
        services.AddScoped<AzureEnvConfig, TenantsAccess>();
        services.AddScoped<LoadEnvironmentVariables, LoadEnvironmentVariables>();
      };
    }

    protected override void Setup()
    {

    }
  }
}