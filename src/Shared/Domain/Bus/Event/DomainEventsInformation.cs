
namespace Shared.Domain.Bus.Event
{
  public class DomainEventsInformation
  {
    private Dictionary<string, Type> domainEventsTypes = [];

    public DomainEventsInformation()
    {
      GetDomainEventTypes().ForEach(eventType => domainEventsTypes.Add(GetEventName(eventType), eventType));
    }
    internal Type FromEventName(string eventName)
    {
      Type domainEventType;

      domainEventsTypes.TryGetValue(eventName, out domainEventType);

      return domainEventType;
    }

    private string GetEventName(Type eventType)
    {
      DomainEvent instance = (DomainEvent)Activator.CreateInstance(eventType);

      return eventType.GetMethod("EventName").Invoke(instance, null).ToString();
    }

    private List<Type> GetDomainEventTypes()
    {
      Type domainEventType = typeof(DomainEvent);

      return AppDomain.CurrentDomain.GetAssemblies()
      .SelectMany(assembles => assembles.GetTypes())
      .Where(type => domainEventType.IsAssignableFrom(type) && !type.IsAbstract).ToList();
    }
  }
}