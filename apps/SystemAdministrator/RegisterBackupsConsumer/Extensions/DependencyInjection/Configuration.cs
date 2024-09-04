using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Bus.Event.RabbitMQ;
using Shared.Infrastructure.Enviroment;

namespace CloudBackupsRecovery.Extensions.DependencyInjection
{
  public static class Configuration
  {
    public static IServiceCollection AddConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
    {
      services.Configure<List<EnvironmentVariables>>(configurationManager.GetSection(EnvironmentVariables.Name));
      services.AddScoped<LoadEnvironmentVariables, LoadEnvironmentVariables>();

      services.Configure<RabbitMQSettings>(configurationManager.GetSection(RabbitMQSettings.Name));


      return services;
    }
  }
}