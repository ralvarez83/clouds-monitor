using Clouds.LastBackups.Infraestructure.Bus.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Shared.Domain.Bus.Event;
using Shared.Infrastructure.Bus.Event.RabbitMQ;
using SharedTest.Domain;

namespace SharedTest.Infrastructure.Bus.Event.RabbitMQ
{
  public class RabbitMQTestUnitCase : InfrastructureTestCase, IDisposable
  {
    private const int MaxAttempts = 5;
    private const int MillisToWaitBetweenRetries = 300;
    protected override Action<IServiceCollection> GetServices()
    {

      return services =>
      {
        var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

        RabbitMQSettings? rabbitMQSettings = configuration.GetSection(RabbitMQSettings.Name).Get<RabbitMQSettings>();
        int postNames = TimeSpan.FromTicks(DateTime.Now.Ticks).Milliseconds;
        if (null == rabbitMQSettings)
          throw new Exception("RabbitMQSettings not found");

        rabbitMQSettings.Exchange = new Exchanges(
          rabbitMQSettings.Exchange.Name + postNames,
          rabbitMQSettings.Exchange.Subscribers.Select(suscriber => new Subscribers(suscriber.QueueName + postNames, suscriber.EventName)).ToArray()
          );
        services.AddScoped(servicesProvider => rabbitMQSettings);

        services.AddScoped(serviceProvider =>
        {
          IOptions<RabbitMQSettings> rabbitMqParams = Options.Create(rabbitMQSettings);
          return new RabbitMQConfig(rabbitMqParams);
        });
        services.AddScoped<RabbitMQPublisher, RabbitMQPublisher>();
        services.AddScoped<EventBus>(serviceProvider =>
        {
          RabbitMQPublisher? publisher = serviceProvider.GetService<RabbitMQPublisher>();
          if (null == publisher)
            throw new Exception("RabbitMQPublisher not found");
          return new RabbitMQEventBus(publisher);
          // return new RabbitMQEventBus(publisher, "test_domain_events_" + TimeSpan.FromTicks(DateTime.Now.Ticks).Milliseconds);
        }
        );
        services.AddScoped<DomainEventsInformation, DomainEventsInformation>();
        services.AddScoped<DomainEventJsonDeserializer, DomainEventJsonDeserializer>();
        services.AddScoped<Subscriber, SubscriberFake>();
        services.AddScoped<Consumer, RabbitMQConsumer>();
      };
    }

    protected override void Setup()
    {

    }

    // Copy from C#-ddd-scheleton by CodelyTV: https://github.com/CodelyTV/csharp-ddd-skeleton
    protected async Task WaitFor(Func<Task<bool>> function)
    {
      var attempts = 0;
      var allOk = false;
      while (attempts < MaxAttempts && !allOk)
        try
        {
          allOk = await function.Invoke();
          if (!allOk) throw new Exception();
        }
        catch (Exception e)
        {
          attempts++;

          if (attempts > MaxAttempts)
            throw new Exception($"Could not assert after some retries. Last error: {e.Message}");

          Thread.Sleep(MillisToWaitBetweenRetries);
        }
    }

    public void Dispose()
    {
      CleanEnvironment();
      Finish();
    }

    private void CleanEnvironment()
    {
      RabbitMQConfig? config = GetService<RabbitMQConfig>();
      if (null == config)
        throw new Exception("El servicio RabbitMQConfig no encontrado");

      RabbitMQSettings? settings = GetService<RabbitMQSettings>();
      if (null == settings)
        throw new Exception("La sección RabbitMQSettings no encontrada");

      IModel channel = config.Channel();
      string exchangeDeadLetterName = RabbitMqExchangeNameFormatter.DeadLetter(settings.Exchange.Name);

      channel.ExchangeDelete(settings.Exchange.Name);
      channel.ExchangeDelete(exchangeDeadLetterName);

      foreach (Subscribers subscriber in settings.Exchange.Subscribers)
      {
        string deadLetterQueueName = RabbitMQQueueNameFormatter.DeadLetter(subscriber.QueueName);

        channel.QueueDelete(subscriber.QueueName);
        channel.QueueDelete(deadLetterQueueName);

      }
    }

  }
}