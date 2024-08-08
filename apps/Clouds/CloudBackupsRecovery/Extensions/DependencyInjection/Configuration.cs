using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Clouds.LastBackups.Infraestructure.Azure;
using Clouds.LastBackups.Infraestructure.Azure.Configuration;
using Clouds.LastBackups.Infraestructure.Bus.RabbitMQ;
using Shared.Infrastructure.Enviroment;
using Clouds.LastBackups.Infraestructure.Repository.MongoDB;

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