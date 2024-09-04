using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Bus.Event;

namespace Shared.Infrastructure.Bus.Event
{
  public class SubscriberInformation
  {
    private Type subscriberType;
    private IServiceScope serviceScope;

    public Type SubscriberType { get; private set; }
    public string QueueName { get; private set; }
    public string EventName { get; private set; }
    public ushort PrefetchCount { get; private set; }
    public SubscriberInformation(Type type, IServiceScope serviceScope, ushort prefetchCount = 10)
    {
      SubscriberType = type;
      PrefetchCount = prefetchCount;
      QueueName = GetQueueName(SubscriberType);
      EventName = GetEventName(SubscriberType);

      this.serviceScope = serviceScope;
    }

    private string GetEventName(Type subscriberType)
    {
      Type interfaceType = subscriberType.GetInterfaces().Where(interfaceSearch => interfaceSearch.Namespace.Equals("Shared.Domain.Bus.Event") && interfaceSearch.Name.Contains("Subscriber")).First()!;
      Type domainEventType = interfaceType.GenericTypeArguments.First()!;

      var instance = Activator.CreateInstance(domainEventType);
      return domainEventType.GetMethod("EventName").Invoke(instance, null).ToString();
    }

    private string GetQueueName(Type subscriberType)
    {
      return $"{subscriberType.Namespace}.{subscriberType.Name}";
    }


    public Subscriber<DomainEvent>? CreateInstance()
    {
      Subscriber<DomainEvent>? subscriber;

      ConstructorInfo constructorInfo = SubscriberType.GetConstructors().First(constructor => constructor.GetParameters().Count() > 0);

      List<object> parameters = constructorInfo.GetParameters().Select(parameter =>
      {
        var instance = this.serviceScope.ServiceProvider.GetService(parameter.ParameterType);
        if (null == instance)
          throw new Exception($"El servicio {parameter.GetType} no encontrado");
        return instance;
      }).ToList();

      subscriber = (Subscriber<DomainEvent>)Activator.CreateInstance(SubscriberType, parameters.ToArray());

      return subscriber;
    }
  }
}