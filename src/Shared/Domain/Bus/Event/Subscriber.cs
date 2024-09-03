namespace Shared.Domain.Bus.Event
{
  public interface Subscriber<out TDomain> where TDomain : DomainEvent
  {
    public Task On(DomainEvent domainEvent);

  }
}