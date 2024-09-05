
using SystemAdministrator.LastBackups.Application.GetAll;
using SystemAdministrator.LastBackups.Domain;

namespace WebAPI.Extensions.DependencyInjection
{
  public static class Application
  {
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
      services.AddScoped<GetAll, GetAll>();
      return services;
    }
  }
}