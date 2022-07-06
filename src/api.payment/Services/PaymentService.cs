namespace api.payment;

public class PaymentService : IPaymentService
{
    private readonly IMessageProducer _messageProducer;
    private readonly ILogger<PaymentService> _logger;

    public PaymentService(IMessageProducer messageProducer, ILogger<PaymentService> logger)
    {
        _messageProducer = messageProducer;
        _logger = logger;
    }

    public async Task<bool> GetMoneyAsync(Order order)
    {
        //do some payment operations and get model
        var payment = new Payment()
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            ProductId = order.ProductId,
            Quantity = order.Quantity,
            CustomerName = order.CustomerName,
            Price = 23.5m * DateTime.UtcNow.Second,
            CreatedOn = DateTime.UtcNow
        };

        _logger.LogInformation("payment received at {date}", DateTime.Now.ToString("O"));

        _messageProducer.Publish(payment, "payment.received");

        return true;
    }

    public async Task RefundAsync(Stock stock)
    {
        //do some payment refund operations and get model
        var payment = new Payment()
        {
            Id = stock.PaymentId,
            OrderId = stock.OrderId,
            ProductId = stock.ProductId,
            Quantity = stock.Quantity,
            CustomerName = stock.CustomerName,
            Price = stock.Price,
            CreatedOn = DateTime.UtcNow
        };

        _messageProducer.Publish(payment, "payment.refunded");

        _logger.LogInformation("payment refunded at {date}", DateTime.Now.ToString("O"));
    }
}