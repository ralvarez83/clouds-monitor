namespace Shared.Infrastructure.Bus.Event.RabbitMQ
{
  public record Subscribers(string QueueName, string EventName, ushort PrefetchCount = 10);
}