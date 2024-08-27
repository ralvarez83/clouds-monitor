using Shared.Domain.Bus.Event;
using Shared.Infrastructure.Bus.Event.RabbitMQ;
using SharedTest.Domain;

namespace SharedTest.Infrastructure.Bus.Event.RabbitMQ
{
  public class ConsumerEventsShould : RabbitMQTestUnitCase
  {
    [Fact]
    public void ConnectTo_ExchangeAndQueue_And_ReadAMessage()
    {
      // Given a exchange and queue exists with a message
      RabbitMQEventBus? rabbitMQEventBus = GetService<EventBus>() as RabbitMQEventBus;
      if (null == rabbitMQEventBus)
        throw new Exception("El servicio RabbitMQEventBus no encontrado");

      DomainEventFake fakeEvent = DomainEventFactory.BuildEventRandom();

      rabbitMQEventBus.Publish([fakeEvent]);

      // When connect to this queue and wait for a message
      RabbitMQConsumer? rabbitMQConsumer = GetService<Consumer>() as RabbitMQConsumer;

      if (null == rabbitMQConsumer)
        throw new Exception("El servicio RabbitMQConsumer no encontrado");

      rabbitMQConsumer.Consume();

      // Then read the message

    }

    // private async Task evaluateEventRead(DomainEventFake[] sentEvents)
    // {

    //   RabbitMQConfig? config = GetService<RabbitMQConfig>();
    //   if (null == config)
    //     throw new Exception("El servicio RabbitMQConfig no encontrado");

    //   RabbitMQSettings? settings = GetService<RabbitMQSettings>();
    //   if (null == settings)
    //     throw new Exception("La sección RabbitMQSettings no encontrada");

    //   string queuName = settings.Exchange.Subscribers.First().QueuName;

    //   IModel channel = config.Channel();
    //   EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
    //   List<string> messagesShouldReceived = sentEvents.Select(DomainEventJsonSerializer.Serialize).ToList();
    //   List<string> messagesReceibed = [];

    //   consumer.Received += (model, ea) =>
    //   {
    //     byte[] body = ea.Body.ToArray();
    //     messagesReceibed.Add(Encoding.UTF8.GetString(body));
    //   };

    //   channel.BasicConsume(queue: queuName,
    //                        autoAck: true,
    //                        consumer: consumer);

    //   await WaitFor(() => Task.Run(() => messagesReceibed.Count < messagesShouldReceived.Count));

    //   Assert.Equal(messagesShouldReceived, messagesReceibed);

    // }
  }
}