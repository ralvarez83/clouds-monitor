// Base on C#-ddd-scheleton by CodelyTV: https://github.com/CodelyTV/csharp-ddd-skeleton
using Shared.Domain.Bus.Event;
using Shared.Infrastructure.Bus.Event.RabbitMQ;

namespace Shared.Infrastructure.Bus.Event.RabbitMQ
{
  public record RabbitMQSettings
  {
    public string HostName { get; set; } = "";
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public int Port { get; set; }
    public int DeliveryLimit { get; set; }
    // public Exchanges? Exchange { get; set; }
    public string ExchangeName { get; set; } = "";
    public const string Name = "RabbitMQ";

  }
}