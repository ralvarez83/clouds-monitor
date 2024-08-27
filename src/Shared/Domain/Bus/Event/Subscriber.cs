namespace Shared.Domain.Bus.Event
{
  public interface Subscriber
  {
    Task On(DomainEvent domainEvent);
  }
}