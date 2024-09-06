using Microsoft.Extensions.DependencyInjection;
using SystemAdministrator.Machines.Application.RegisterLastBackups;

namespace CloudBackupsRecovery.Extensions.DependencyInjection
{
  public static class Application
  {
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
      services.AddScoped<RegisterLastBackup, RegisterLastBackup>();

      return services;
    }
  }
}