using shared;

namespace api.stock;

public class StockService : IStockService
{
    private readonly IMessageProducer _messageProducer;
    private readonly ILogger<StockService> _logger;

    public StockService(IMessageProducer messageProducer, ILogger<StockService> logger)
    {
        _messageProducer = messageProducer;
        _logger = logger;
    }

    public async Task<bool> ReduceAsync(Payment payment)
    {
        //do some stock reduce operations and get model
        var stock = new Stock()
        {
            Id = Guid.NewGuid(),
            PaymentId = payment.Id,
            OrderId = payment.OrderId,
            ProductId = payment.ProductId,
            Quantity = payment.Quantity,
            Price = payment.Price,
            CustomerName = payment.CustomerName,
            CreatedOn = DateTime.UtcNow
        };

        if (stock.Quantity < 5) //fake control for scenario
        {
            _messageProducer.Publish(stock, "stock.reduced");

            _logger.LogInformation("stock reduced at {date}", DateTime.Now.ToString("O"));

            return true;
        }
        else
        {
            _messageProducer.Publish(stock, "stock.outofstock");

            _logger.LogInformation("stock reduce failed at {date}", DateTime.Now.ToString("O"));

            return false;
        }
    }
}