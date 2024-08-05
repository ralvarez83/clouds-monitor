using Microsoft.Extensions.DependencyInjection;
using Clouds.Backups.Domain;
using Clouds.Backups.Infraestructure.Azure;

namespace CloudBackupsRecovery.Extensions.DependencyInjection
{
  public static class Application
  {
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

      services.AddTransient<BackupsCloudAccess, AzureBackupsAccess>();
      // services.AddTransient<BackupsGetAllLastNDays, BackupsGetAllLastNDays>();
      // services.AddTransient<BackupsGetLast, BackupsGetLast>();

      //services.AddQueryServices(AssemblyHelper.GetInstance(Assemblies.Mooc));
      return services;
    }
  }
}