namespace Shared.Domain.Bus.Event
{
  public abstract class Subscriber<TDomain> where TDomain : DomainEvent
  {
    public abstract Task On(DomainEvent domainEvent);

    public string GetEventName()
    {
      Type domainEventType = typeof(TDomain);
      return DomainEventsInformation.GetEventName(domainEventType);
    }
  }
}