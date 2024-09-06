using Microsoft.Extensions.DependencyInjection;
using Clouds.LastBackups.Application.UpdateLastBackups;
using Clouds.LastBackups.Application.GetCloudLastBackups;

namespace CloudBackupsRecovery.Extensions.DependencyInjection
{
  public static class Application
  {
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

      services.AddTransient<UpdateLastBackups, UpdateLastBackups>();
      services.AddTransient<GetCloudLastBackups, GetCloudLastBackups>();

      return services;
    }
  }
}