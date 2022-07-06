using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace api.payment;

public class StockOutOfStockMessageConsumer : BackgroundService
{
    private readonly IModel _channel;
    private readonly IConnection _connection;
    private readonly IPaymentService _paymentService;

    public StockOutOfStockMessageConsumer(IPaymentService paymentService)
    {
        _paymentService = paymentService;

        var factory = new ConnectionFactory
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672")
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: "stock.outofstock", durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var result = JsonSerializer.Deserialize<Stock>(message);

            await _paymentService.RefundAsync(result);

            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        _channel.BasicConsume("stock.outofstock", false, consumer);
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();

        base.Dispose();
    }
}