using SystemAdministrator.LastBackups.Application.BackupsGetLast;
using SystemAdministrator.LastBackups.Application.BackupsGetLastNDays;
using SystemAdministrator.LastBackups.Domain;

namespace WebAPI.Extensions.DependencyInjection
{
  public static class Application
  {
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

      // services.AddTransient<BackupsRepository, AzureBackupsRepository>();
      services.AddTransient<BackupsGetAllLastNDays, BackupsGetAllLastNDays>();
      services.AddTransient<BackupsGetLast, BackupsGetLast>();

      //services.AddQueryServices(AssemblyHelper.GetInstance(Assemblies.Mooc));
      return services;
    }
  }
}