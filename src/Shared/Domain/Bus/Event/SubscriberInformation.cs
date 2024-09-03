using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;

namespace Shared.Domain.Bus.Event
{
  public class SubscriberInformation
  {
    private Type subscriberType;
    private IServiceProvider serviceProvider;

    public Type Type { get; }
    public string QueueName { get; }
    public string EventName { get; }
    public ushort PrefetchCount { get; }
    public SubscriberInformation(Type type, IServiceProvider serviceProvider, ushort prefetchCount = 10)
    {
      Type = type;
      PrefetchCount = prefetchCount;
      QueueName = $"{type.Namespace}.{type.Name}";
      var instance = Activator.CreateInstance(type);
      EventName = type.GetMethod("GetEventName").Invoke(instance, null).ToString();
      this.serviceProvider = serviceProvider;
    }


    public Subscriber<DomainEvent>? CreateInstance()
    {
      Subscriber<DomainEvent>? subscriber;

      ConstructorInfo constructorInfo = Type.GetConstructors().First(constructor => constructor.GetParameters().Count() > 0);

      List<object> parameters = constructorInfo.GetParameters().Select(parameter =>
      {
        var instance = this.serviceProvider.GetService(parameter.GetType());
        if (null == instance)
          throw new Exception($"El servicio {parameter.GetType} no encontrado");
        return instance;
      }).ToList();

      subscriber = (Subscriber<DomainEvent>)Activator.CreateInstance(Type, parameters);

      return subscriber;
    }
  }
}