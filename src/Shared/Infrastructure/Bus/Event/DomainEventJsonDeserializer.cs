using Newtonsoft.Json;
using Shared.Domain.ValueObjects;
using Shared.Infrastructure.Bus.Event;
using System.Reflection;

// Base on C#-ddd-scheleton by CodelyTV: https://github.com/CodelyTV/csharp-ddd-skeleton
namespace Shared.Domain.Bus.Event
{
    public class DomainEventJsonDeserializer(DomainEventsInformation information)
    {
        private readonly DomainEventsInformation information = information;

        public DomainEvent Deserialize(string body)
        {
            var eventData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(body);

            var data = eventData["data"];
            var attributes = JsonConvert.DeserializeObject<Dictionary<string, string>>(data["attributes"].ToString());

            Type domainEventType = information.FromEventName((string)data["type"]);

            DomainEvent instance = (DomainEvent)Activator.CreateInstance(domainEventType);

            DomainEvent domainEvent = (DomainEvent)domainEventType
                .GetTypeInfo()
                .GetDeclaredMethod(nameof(DomainEvent.FromPrimitives))
                .Invoke(instance, new object[]
                {
                    attributes["id"],
                    attributes,
                    data["id"],
                    new SimpleDate((DateTime) data["occurred_on"]).ToString()
                });

            return domainEvent;
        }
    }
}
