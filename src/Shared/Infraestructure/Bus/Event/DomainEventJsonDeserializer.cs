using System.Reflection;
using System.Net.Http.Json;

// Base on C#-ddd-scheleton by CodelyTV: https://github.com/CodelyTV/csharp-ddd-skeleton
namespace Shared.Domain.Bus.Event
{
    public class DomainEventJsonDeserializer
    {
        // private readonly DomainEventsInformation information;

        // public DomainEventJsonDeserializer(DomainEventsInformation information)
        // {
        //     this.information = information;
        // }

        // public DomainEvent Deserialize(string body)
        // {
        //     var eventData = JsonContent.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(body);

        //     var data = eventData["data"];
        //     var attributes = JsonConvert.DeserializeObject<Dictionary<string, string>>(data["attributes"].ToString());

        //     var domainEventType = information.ForName((string)data["type"]);

        //     var instance = (DomainEvent)Activator.CreateInstance(domainEventType);

        //     var domainEvent = (DomainEvent)domainEventType
        //         .GetTypeInfo()
        //         .GetDeclaredMethod(nameof(DomainEvent.FromPrimitives))
        //         .Invoke(instance, new object[]
        //         {
        //             attributes["id"],
        //             attributes,
        //             data["id"].ToString(),
        //             data["occurred_on"].ToString()
        //         });

        //     return domainEvent;
        // }
    }
}
