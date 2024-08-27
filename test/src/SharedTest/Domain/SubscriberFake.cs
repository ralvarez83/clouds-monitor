using Shared.Domain.Bus.Event;

namespace SharedTest.Domain
{
  public class SubscriberFake : Subscriber
  {

    public Task On(DomainEvent domainEvent)
    {
      Assert.NotNull(domainEvent);

      return Task.CompletedTask;
    }
  }
}