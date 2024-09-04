using Microsoft.Extensions.DependencyInjection;
using SystemAdministrator.LastBackups.Application.Register;

namespace CloudBackupsRecovery.Extensions.DependencyInjection
{
  public static class Application
  {
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
      services.AddScoped<RegisterBackup, RegisterBackup>();

      return services;
    }
  }
}