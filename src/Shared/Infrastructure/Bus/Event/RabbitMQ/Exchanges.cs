namespace Shared.Infrastructure.Bus.Event.RabbitMQ
{
  public record Exchanges(string Name, Subscribers[] Subscribers);
}