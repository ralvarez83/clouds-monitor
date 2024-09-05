// using Shared.Backups.Infraestructure.Azure;
// using Shared.Backups.Infraestructure.Azure.Configuration;

namespace WebAPI.Extensions.DependencyInjection
{
  public static class Configuration
  {
    public static IServiceCollection AddConfiguration (this IServiceCollection services, ConfigurationManager configurationManager){

      return services;
    }
  }
}