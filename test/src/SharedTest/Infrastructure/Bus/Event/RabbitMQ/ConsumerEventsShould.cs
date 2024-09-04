using RabbitMQ.Client;
using Shared.Domain.Bus.Event;
using Shared.Infrastructure.Bus.Event;
using Shared.Infrastructure.Bus.Event.RabbitMQ;
using SharedTest.Domain;

namespace SharedTest.Infrastructure.Bus.Event.RabbitMQ
{
  public class ConsumerEventsShould : RabbitMQTestUnitCase
  {
    private List<SubscriberInformation> subscribersInformation;
    public ConsumerEventsShould()
    {
      SubscribersInformation? subscribersInformation = GetService<SubscribersInformation>();
      if (null == subscribersInformation)
        throw new Exception("La sección SubscribersInformation no encontrada");
      this.subscribersInformation = subscribersInformation.GetSubscribers();
    }

    [Fact]
    public async Task ConnectTo_ExchangeAndQueue_And_ReadAMessageAsync()
    {
      // Given a exchange and queue exists with a message
      GivenAnExchangeAndQueueHasAMessage();

      // When connect to this queue and wait for a message
      await WhenConsumeMessagesAsync();

      // Then read the message and DeadLetter Queue is empty

      string queue = subscribersInformation.First().QueueName;
      var messageNumber = GetMessageCount(queue);
      Assert.Equal(0, messageNumber);

      await DeadLetterHasMessages(0);
    }

    [Fact]
    public async void SubscriberSendAndOnlyOneException_DeadLetterShouldBeEmpty()
    {
      // Given a exchange and queue exists with a message
      GivenAnExchangeAndQueueHasAMessage();

      // When the subscriber execution create an exception
      Environment.SetEnvironmentVariable(SubscriberFake.CREATE_EXCEPTION, SubscriberFake.CREATE_EXCEPTION);
      await WhenConsumeMessagesAsync();
      Environment.SetEnvironmentVariable(SubscriberFake.CREATE_EXCEPTION, null);

      // Then the retry queue should shouldBeEmpty

      await DeadLetterHasMessages(0);

    }

    [Fact]
    public async Task SubscriberSendAndExceptionMoreThanMaxTries_ShoudMessageSentToDeadLetterAsync()
    {
      // Given a exchange and queue exists with a message
      GivenAnExchangeAndQueueHasAMessage();

      // When the subscriber execution create an exception more than MaxRetries (2)
      Environment.SetEnvironmentVariable(SubscriberFake.CREATE_EXCEPTION, SubscriberFake.CREATE_EXCEPTION);
      await WhenConsumeMessagesAsync();

      // Then the dead letter queue should have 1 message

      await DeadLetterHasMessages(1);

      Environment.SetEnvironmentVariable(SubscriberFake.CREATE_EXCEPTION, null);
    }

    private void GivenAnExchangeAndQueueHasAMessage()
    {
      RabbitMQEventBus? rabbitMQEventBus = GetService<EventBus>() as RabbitMQEventBus;
      if (null == rabbitMQEventBus)
        throw new Exception("El servicio RabbitMQEventBus no encontrado");

      DomainEventFake fakeEvent = DomainEventFactory.BuildEventRandom();

      rabbitMQEventBus.Publish([fakeEvent]);
    }

    private async Task WhenConsumeMessagesAsync()
    {
      RabbitMQConsumer? rabbitMQConsumer = GetService<Consumer>() as RabbitMQConsumer;

      if (null == rabbitMQConsumer)
        throw new Exception("El servicio RabbitMQConsumer no encontrado");

      await rabbitMQConsumer.Consume();
    }

    private int GetMessageCount(string queueName)
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
    private async Task DeadLetterHasMessages(int numberOfMessages)
    {
      var messageNumber = -1;

      await WaitFor(() => Task.Run(() =>
      {
        string deadLetterQueue = RabbitMQQueueNameFormatter.DeadLetter(subscribersInformation.First().QueueName);
        messageNumber = GetMessageCount(deadLetterQueue);

        if (0 == numberOfMessages)
          return false;

        return messageNumber == numberOfMessages;
      })
      );

      Assert.Equal(numberOfMessages, messageNumber);
    }
  }
}