using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Bus.Event;
using Shared.Infrastructure.Bus.Event;
using Shared.Infrastructure.Bus.Event.RabbitMQ;
using Shared.Infrastructure.Repository.MongoDB;
using SystemAdministrator.Machines.Domain;
using SystemAdministrator.Machines.Infrastructure.Repository.MongoDB;

namespace CloudBackupsRecovery.Extensions.DependencyInjection
{
  public static class Infraestructure
  {
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, ConfigurationManager configurationManager)
    {
      //Bus Infraestructure: Events
      services.AddScoped<RabbitMQConfig, RabbitMQConfig>();
      services.AddScoped<DomainEventJsonDeserializer, DomainEventJsonDeserializer>();
      services.AddScoped<SubscribersInformation, SubscribersInformation>();
      services.AddScoped<DomainEventsInformation, DomainEventsInformation>();
      services.AddScoped<RabbitMQConsumer, RabbitMQConsumer>();

      // Repository DataBase
      services.AddScoped<MachinesRepository, MongoDBMachinesRepository>();

      services.AddDbContext<MachinesContext>(options =>
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