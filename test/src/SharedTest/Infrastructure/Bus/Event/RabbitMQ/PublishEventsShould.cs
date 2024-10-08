using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Domain.Bus.Event;
using Shared.Infrastructure.Bus.Event;
using Shared.Infrastructure.Bus.Event.RabbitMQ;
using SharedTest.Domain;

namespace SharedTest.Infrastructure.Bus.Event.RabbitMQ
{
  public class PublishEventsShould : RabbitMQTestUnitCase
  {
    [Fact]
    public void EventListEmptyNoPublishEvents()
    {
      RabbitMQEventBus? rabbitMQEventBus = GetService<EventBus>() as RabbitMQEventBus;
      if (null == rabbitMQEventBus)
        throw new Exception("El servicio RabbitMQEventBus no encontrado");

      // Given event list is empty
      List<DomainEvent> events = [];

      // When publish events
      rabbitMQEventBus.Publish(events);

      // Then any events was published


    }
    [Fact]
    public async Task OneEventPublish()
    {
      RabbitMQEventBus? rabbitMQEventBus = GetService<EventBus>() as RabbitMQEventBus;
      if (null == rabbitMQEventBus)
        throw new Exception("El servicio RabbitMQEventBus no encontrado");

      // Given event list is empty
      DomainEventFake fakeEvent = DomainEventFactory.BuildEventRandom();

      // When publish events
      rabbitMQEventBus.Publish([fakeEvent]);

      // Then any events was published
      await evaluateEventSent([fakeEvent]);

    }

    [Fact]
    public async Task ListOfEventsPublish()
    {
      RabbitMQEventBus? rabbitMQEventBus = GetService<EventBus>() as RabbitMQEventBus;
      if (null == rabbitMQEventBus)
        throw new Exception("El servicio RabbitMQEventBus no encontrado");

      // Given event list is empty
      DomainEventFake[] fakeEvents = DomainEventFactory.BuildArrayOfBackupsRandom();

      // When publish events
      rabbitMQEventBus.Publish([.. fakeEvents]);

      // Then any events was published
      await evaluateEventSent(fakeEvents);

    }

    private async Task evaluateEventSent(DomainEventFake[] sentEvents)
    {

      RabbitMQConfig? config = GetService<RabbitMQConfig>();
      if (null == config)
        throw new Exception("El servicio RabbitMQConfig no encontrado");

      SubscribersInformation? subscribersInformation = GetService<SubscribersInformation>();
      if (null == subscribersInformation)
        throw new Exception("La sección SubscribersInformation no encontrada");

      string queuName = subscribersInformation.GetSubscribers().First().QueueName;

      IModel channel = config.Channel();
      EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
      List<string> messagesShouldReceived = sentEvents.Select(DomainEventJsonSerializer.Serialize).ToList();
      List<string> messagesReceibed = [];

      consumer.Received += (model, ea) =>
      {
        byte[] body = ea.Body.ToArray();
        messagesReceibed.Add(Encoding.UTF8.GetString(body));
      };

      channel.BasicConsume(queue: queuName,
                           autoAck: true,
                           consumer: consumer);

      await WaitFor(() => Task.Run(() => messagesReceibed.Count < messagesShouldReceived.Count));

      Assert.Equal(messagesShouldReceived, messagesReceibed);

    }


  }
}