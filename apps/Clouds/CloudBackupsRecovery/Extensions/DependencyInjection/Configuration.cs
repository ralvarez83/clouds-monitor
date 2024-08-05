using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Clouds.Backups.Infraestructure.Azure;
using Clouds.Backups.Infraestructure.Azure.Configuration;

namespace CloudBackupsRecovery.Extensions.DependencyInjection
{
  public static class Configuration
  {
    public static IServiceCollection AddConfiguration (this IServiceCollection services, ConfigurationManager configurationManager){
      services.Configure<List<Suscriptions>>(configurationManager.GetSection(Suscriptions.Name));
      services.AddSingleton<AzureEnvConfig, TenantsAccess>();

      return services;
    }
  }
}