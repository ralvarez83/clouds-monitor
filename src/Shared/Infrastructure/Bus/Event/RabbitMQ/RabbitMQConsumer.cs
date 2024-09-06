using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Domain.Bus.Event;

namespace Shared.Infrastructure.Bus.Event.RabbitMQ
{
  public class RabbitMQConsumer(RabbitMQConfig config, DomainEventJsonDeserializer deserializer, SubscribersInformation subscribersInformation) : Consumer
  {
    private readonly RabbitMQConfig config = config;
    private readonly DomainEventJsonDeserializer deserializer = deserializer;
    private readonly SubscribersInformation subscribersInformation = subscribersInformation;

    public Task Consume()
    {
      List<SubscriberInformation> subscribers = subscribersInformation.GetSubscribers();
      subscribers.ForEach(ConsumeMessages);

      return Task.CompletedTask;
    }

    private void ConsumeMessages(SubscriberInformation subscriberInformation)
    {
      IModel channel = config.Channel();
      EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

      consumer.Received += async (model, eventArgs) =>
      {
        string message = Encoding.UTF8.GetString(eventArgs.Body.Span);
        DomainEvent domainEvent = deserializer.Deserialize(message);

        try
        {
          await subscriberInformation.CreateInstance().On(domainEvent);
          channel.BasicAck(eventArgs.DeliveryTag, false);
        }
        catch (Exception ex)
        {
          channel.BasicNack(eventArgs.DeliveryTag, false, true);
        }

      };

      var consumerId = channel.BasicConsume(subscriberInformation.QueueName, false, consumer);

    }
  }
}