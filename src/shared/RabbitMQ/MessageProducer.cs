using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace shared;

public class MessageProducer : IMessageProducer
{
    public void Publish<T>(T message, string queue)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672")
        };

        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue, durable: false, exclusive: false, autoDelete: false, null);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(exchange: "", routingKey: queue, body: body);
    }
}