using MediatR;
using Shared.Domain.Bus.Query;
using Shared.Infraestructure.Bus.Query.MediatR;

namespace WebAPI.Extensions.DependencyInjection
{
  public static class Infraestructure
  {
    public static IServiceCollection AddInfraestructure(this IServiceCollection services)
    {
      services.AddTransient<Mediator, Mediator>();

      services.AddSingleton<QueryBus, MediatRQueryBus>();

      return services;
    }
  }
}