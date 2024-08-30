// Base on C#-ddd-scheleton by CodelyTV: https://github.com/CodelyTV/csharp-ddd-skeleton
namespace Shared.Infrastructure.Bus.Event.RabbitMQ
{
    public static class RabbitMqExchangeNameFormatter
    {
        public static string DeadLetter(string exchangeName)
        {
            return $"dead_letter-{exchangeName}";
        }
    }
}
