using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Clouds.LastBackups.Infrastructure.Azure;
using Clouds.LastBackups.Infrastructure.Azure.Configuration;
using Shared.Infrastructure.Enviroment;
using Shared.Infrastructure.Bus.Event.RabbitMQ;

namespace CloudBackupsRecovery.Extensions.DependencyInjection
{
  public static class Configuration
  {
    public static IServiceCollection AddConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
    {
      services.Configure<List<EnvironmentVariables>>(configurationManager.GetSection(EnvironmentVariables.Name));
      services.AddScoped<LoadEnvironmentVariables, LoadEnvironmentVariables>();

      services.Configure<List<Suscriptions>>(configurationManager.GetSection(Suscriptions.Name));
      services.AddSingleton<AzureEnvConfig, TenantsAccess>();

      services.Configure<RabbitMQSettings>(configurationManager.GetSection(RabbitMQSettings.Name));


      return services;
    }
  }
}