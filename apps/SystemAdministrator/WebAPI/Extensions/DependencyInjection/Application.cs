
using SystemAdministrator.Machines.Application.GetAll;
using SystemAdministrator.Machines.Domain;

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