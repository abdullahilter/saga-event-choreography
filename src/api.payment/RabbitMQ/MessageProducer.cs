using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace api.payment;

public class MessageProducer : IMessageProducer
{
    private readonly IOptions<RabbitMQOptions> _options;

    public MessageProducer(IOptions<RabbitMQOptions> options)
    {
        _options = options;
    }

    public void Publish<T>(T message, string queue)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(_options.Value.Uri)
        };

        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue, durable: false, exclusive: false, autoDelete: false, null);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(exchange: "", routingKey: queue, body: body);
    }
}