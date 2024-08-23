using System.Collections.Generic;
using System.Text.Json;
using Shared.Domain.Bus.Event;

// Base on C#-ddd-scheleton by CodelyTV: https://github.com/CodelyTV/csharp-ddd-skeleton
namespace Shared.Infrastructure.Bus.Event
{
    public static class DomainEventJsonSerializer
    {
        public static string Serialize(DomainEvent domainEvent)
        {
            if (domainEvent == null) return "";

            Dictionary<string, string> attributes = domainEvent.ToPrimitives();

            attributes.Add("id", domainEvent.Id);

            return JsonSerializer.Serialize(new Dictionary<string, Dictionary<string, object>>
            {
                {
                    "data", new Dictionary<string, object>
                    {
                        {"id", domainEvent.EventId},
                        {"type", domainEvent.EventName()},
                        {"occurred_on", domainEvent.OccurredOn},
                        {"attributes", attributes}
                    }
                },
                {"meta", new Dictionary<string, object>()}
            });
        }
    }
}
