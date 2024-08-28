using System.Text;
using Clouds.LastBackups.Infraestructure.Bus.RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Domain.Bus.Event;

namespace Shared.Infrastructure.Bus.Event.RabbitMQ
{
  public class RabbitMQConsumer : Consumer
  {
    private readonly RabbitMQConfig config;
    private readonly RabbitMQSettings settings;
    private readonly DomainEventJsonDeserializer deserializer;
    private readonly Subscriber suscriber;

    public RabbitMQConsumer(RabbitMQConfig config, RabbitMQSettings settings, DomainEventJsonDeserializer deserializer, Subscriber suscriber)
    {
      this.config = config;
      this.settings = settings;
      this.deserializer = deserializer;
      this.suscriber = suscriber;
    }


    public Task Consume()
    {
      settings.Exchange.Subscribers.ToList().ForEach(ConsumeMessages);

      return Task.CompletedTask;
    }

    private void DeclareQueue(IModel channel, string queue)
    {
      channel.QueueDeclare(queue,
                          durable: true,
                          exclusive: false,
                          autoDelete: false
      );
    }

    private void ConsumeMessages(Subscribers subscriber)
    {
      IModel channel = config.Channel();
      EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

      DeclareQueue(channel, subscriber.QueuName);
      channel.BasicQos(0, subscriber.PrefetchCount, false);

      consumer.Received += async (model, eventArgs) =>
      {
        string message = Encoding.UTF8.GetString(eventArgs.Body.Span);
        DomainEvent domainEvent = deserializer.Deserialize(message);

        await suscriber.On(domainEvent);

        channel.BasicAck(eventArgs.DeliveryTag, false);
      };

      var consumerId = channel.BasicConsume(subscriber.QueuName, false, consumer);

    }
  }
}