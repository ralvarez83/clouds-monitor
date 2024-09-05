using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Bus.Query;
using Shared.Infrastructure.Bus.Query.MediatR;
using Shared.Infrastructure.Repository.MongoDB;
using SystemAdministrator.LastBackups.Domain;
using SystemAdministrator.LastBackups.Infrastructure.Bus.Query.MediatR.GetAll;
using SystemAdministrator.LastBackups.Infrastructure.Repository.MongoDB;

namespace WebAPI.Extensions.DependencyInjection
{
  public static class Infraestructure
  {
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, ConfigurationManager configurationManager)
    {

      //Bus Infraestructure: Query & Command
      services.AddScoped<Mediator, Mediator>();
      services.AddScoped<IMediatRQueryDirectoryWrapper, MediatRQueryDirectoryWrapper>();
      services.AddScoped<QueryBus, MediatRQueryBus>();
      services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<MediatRGetCloudLastBackupsHandler>());

      // Repository DataBase
      services.AddScoped<LastBackupsRepository, MongoDBBackupsRepository>();

      services.AddDbContext<BackupsContext>(options =>
      {
        MongoDBSettings? mongoDBSettings = configurationManager.GetSection(MongoDBSettings.Name).Get<MongoDBSettings>();
        if (null == mongoDBSettings)
          throw new Exception("Section MongoDBSettings not found");

        options.UseMongoDB(mongoDBSettings.MongoDBURI ?? "", mongoDBSettings.DatabaseName ?? "");
      });
      return services;
    }
  }
}