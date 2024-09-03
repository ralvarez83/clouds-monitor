using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
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
      IServiceScope scope = serviceProvider.CreateScope();

      Type[] shared = AppDomain.CurrentDomain.GetAssemblies()
       .Where(assembles => assembles.FullName.Contains("Shared"))
       .SelectMany(assembles => assembles.GetTypes()).ToArray<Type>();

      List<Type> subscribersType = AppDomain.CurrentDomain.GetAssemblies()
      .SelectMany(assembles => assembles.GetTypes())
      .Where(type => null != type.GetInterface(subscriberType.Name) && !type.IsAbstract).ToList();
      // .Where(type => null != type.BaseType && type.BaseType.Name.Equals(subscriberType.Name) && !type.IsAbstract).ToList();

      return subscribersType.Select(subscriberType => new SubscriberInformation(subscriberType, scope)).ToList();
    }
  }
}