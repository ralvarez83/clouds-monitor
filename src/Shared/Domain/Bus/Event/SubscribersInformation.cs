using RabbitMQ.Client.Events;

namespace Shared.Domain.Bus.Event
{
  public class SubscribersInformation(IServiceProvider serviceProvider)
  {
    private readonly IServiceProvider serviceProvider = serviceProvider;

    private static List<SubscriberInformation> subscribers { get; set; }

    public List<SubscriberInformation> GetSubscribers()
    {
      if (null == subscribers)
      {
        subscribers = GetAllSubscribers();
      }
      return subscribers;
    }

    private List<SubscriberInformation> GetAllSubscribers()
    {
      Type subscriberType = typeof(Subscriber<>);

      List<Type> subscribersType = AppDomain.CurrentDomain.GetAssemblies()
      .SelectMany(assembles => assembles.GetTypes())
      .Where(type => null != type.BaseType && subscriberType.Name.Equals(type.BaseType.Name) && !type.IsAbstract).ToList();

      return subscribersType.Select(subscriberType => new SubscriberInformation(subscriberType, serviceProvider)).ToList();
    }
  }
}