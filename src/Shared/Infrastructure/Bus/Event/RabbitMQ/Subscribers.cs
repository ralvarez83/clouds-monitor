namespace Shared.Infrastructure.Bus.Event.RabbitMQ
{
  public record Subscribers(string QueuName, string EventName, ushort PrefetchCount = 10);
}