// Base on C#-ddd-scheleton by CodelyTV: https://github.com/CodelyTV/csharp-ddd-skeleton
namespace Shared.Infrastructure.Bus.Event.RabbitMQ
{
    public static class RabbitMQQueueNameFormatter
    {
        public static string DeadLetter(string queueName)
        {
            return $"dead_letter-{queueName}";
        }
    }
}
