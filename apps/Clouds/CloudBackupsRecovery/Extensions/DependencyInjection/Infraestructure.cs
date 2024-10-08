using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Clouds.LastBackups.Domain;
using Clouds.LastBackups.Infrastructure.Azure;
using Clouds.LastBackups.Infrastructure.Bus.Command.MediatR.UpdateLastBackups;
using Clouds.LastBackups.Infrastructure.Repository.MongoDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Bus.Command;
using Shared.Domain.Bus.Event;
using Shared.Domain.Bus.Query;
using Shared.Infrastructure.Bus.Command.MediatR;
using Shared.Infrastructure.Bus.Event;
using Shared.Infrastructure.Bus.Event.RabbitMQ;
using Shared.Infrastructure.Bus.Query.MediatR;
using Shared.Infrastructure.Repository.MongoDB;

namespace CloudBackupsRecovery.Extensions.DependencyInjection
{
  public static class Infraestructure
  {
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, ConfigurationManager configurationManager)
    {

      //Bus Infraestructure: Query & Command
      services.AddScoped<Mediator, Mediator>();
      services.AddScoped<IMediatRCommandDirectoryWrapper, MediatRCommandDirectoryWrapper>();
      services.AddScoped<IMediatRQueryDirectoryWrapper, MediatRQueryDirectoryWrapper>();
      services.AddScoped<CommandBus, MediatRCommandBus>();
      services.AddScoped<QueryBus, MediatRQueryBus>();
      services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<MediatRUpdateLastBackupsHandler>());

      //Bus Infraestructure: Events
      services.AddScoped<SubscribersInformation, SubscribersInformation>();
      services.AddScoped<RabbitMQConfig, RabbitMQConfig>();
      services.AddScoped<RabbitMQPublisher, RabbitMQPublisher>();
      services.AddScoped<EventBus, RabbitMQEventBus>();

      // Repository DataBase
      services.AddScoped<LastBackupsRepository, MongoDBLastBackupRepository>();

      services.AddDbContext<LastBackupsStatusContext>(options =>
      {
        MongoDBSettings? mongoDBSettings = configurationManager.GetSection(MongoDBSettings.Name).Get<MongoDBSettings>();
        if (null == mongoDBSettings)
          throw new Exception("Section MongoDBSettings not found");

        options.UseMongoDB(mongoDBSettings.MongoDBURI ?? "", mongoDBSettings.DatabaseName ?? "");
      });

      // Repository Cloud
      services.AddScoped<LastBackupsCloudAccess, AzureBackupsAccess>();
      // Add services to the container.
      services.AddAzureClients(static x =>
      {
        TokenCredential cred = new DefaultAzureCredential();
        ArmClient client = new ArmClient(cred);
        x.AddArmClient(client.GetDefaultSubscription().ToString());
        x.UseCredential(new DefaultAzureCredential());
      });

      return services;
    }
  }
}