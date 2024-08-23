// Base on C#-ddd-scheleton by CodelyTV: https://github.com/CodelyTV/csharp-ddd-skeleton
using Shared.Infrastructure.Bus.Event.RabbitMQ;

namespace Clouds.LastBackups.Infraestructure.Bus.RabbitMQ
{
  public record RabbitMQSettings
  {
    public string HostName { get; set; } = "";
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public int Port { get; set; }
    public Exchanges? Exchange { get; set; }
    public const string Name = "RabbitMQ";
  }
}