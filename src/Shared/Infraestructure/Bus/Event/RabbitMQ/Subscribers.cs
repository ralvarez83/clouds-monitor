namespace Shared.Infraestructure.Bus.Event.RabbitMQ
{
  public record Subscribers(string QueuName, string EventName);
}