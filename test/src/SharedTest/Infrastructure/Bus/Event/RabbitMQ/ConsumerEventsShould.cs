using Clouds.LastBackups.Infraestructure.Bus.RabbitMQ;
using RabbitMQ.Client;
using Shared.Domain.Bus.Event;
using Shared.Infrastructure.Bus.Event.RabbitMQ;
using SharedTest.Domain;

namespace SharedTest.Infrastructure.Bus.Event.RabbitMQ
{
  public class ConsumerEventsShould : RabbitMQTestUnitCase
  {
    [Fact]
    public async Task ConnectTo_ExchangeAndQueue_And_ReadAMessageAsync()
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

      await rabbitMQConsumer.Consume();

      // Then read the message
      RabbitMQSettings? settings = GetService<RabbitMQSettings>();
      if (null == settings)
        throw new Exception("La sección RabbitMQSettings no encontrada");

      string queue = settings.Exchange.Subscribers.First().QueuName;
      var messageNumber = GetMessageCount(queue);
      Assert.Equal(0, messageNumber);
    }


    public int GetMessageCount(string queueName)
    {
      RabbitMQConfig? config = GetService<RabbitMQConfig>();
      if (null == config)
        throw new Exception("El servicio RabbitMQConfig no encontrado");

      int numberOfMessages = -1;
      using (IModel channel = config.Connection().CreateModel())
      {
        numberOfMessages = (int)channel.MessageCount(queueName);
      }

      return numberOfMessages;
    }
  }
}