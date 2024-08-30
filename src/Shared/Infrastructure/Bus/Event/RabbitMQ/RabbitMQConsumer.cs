using System.Text;
using Clouds.LastBackups.Infraestructure.Bus.RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Domain.Bus.Event;

namespace Shared.Infrastructure.Bus.Event.RabbitMQ
{
  public class RabbitMQConsumer(RabbitMQConfig config, RabbitMQSettings settings, DomainEventJsonDeserializer deserializer, Subscriber suscriber) : Consumer
  {
    private readonly RabbitMQConfig config = config;
    private readonly RabbitMQSettings settings = settings;
    private readonly DomainEventJsonDeserializer deserializer = deserializer;
    private readonly Subscriber suscriber = suscriber;

    public Task Consume()
    {
      settings.Exchange.Subscribers.ToList().ForEach(suscriber =>
      {
        ConsumeMessages(suscriber.QueueName, suscriber.PrefetchCount);
        //ConsumeMessages(RabbitMQQueueNameFormatter.Retry(suscriber.QueuName), suscriber.PrefetchCount);
      });

      return Task.CompletedTask;
    }

    private void ConsumeMessages(string queue, ushort prefetchCount = 10)
    {
      IModel channel = config.Channel();
      EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

      channel.BasicQos(0, prefetchCount, false);

      consumer.Received += async (model, eventArgs) =>
      {
        string message = Encoding.UTF8.GetString(eventArgs.Body.Span);
        DomainEvent domainEvent = deserializer.Deserialize(message);

        try
        {
          await suscriber.On(domainEvent);
          channel.BasicAck(eventArgs.DeliveryTag, false);
        }
        catch (Exception)
        {
          channel.BasicNack(eventArgs.DeliveryTag, false, true);
        }

      };

      var consumerId = channel.BasicConsume(queue, false, consumer);

    }
  }
}