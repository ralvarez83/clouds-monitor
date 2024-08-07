namespace Shared.Infraestructure.Bus.Event.RabbitMQ
{
  public record Exchanges(string Name, Subscribers[] Subscribers);
}