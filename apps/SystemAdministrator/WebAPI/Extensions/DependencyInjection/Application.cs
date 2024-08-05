using SystemAdministrator.Backups.Application.BackupsGetLast;
using SystemAdministrator.Backups.Application.BackupsGetLastNDays;
using SystemAdministrator.Backups.Domain;

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