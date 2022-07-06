namespace api.order;

public class OrderService : IOrderService
{
    private readonly IMessageProducer _messageProducer;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IMessageProducer messageProducer, ILogger<OrderService> logger)
    {
        _messageProducer = messageProducer;
        _logger = logger;
    }

    public async Task<Order> CreateAsync(OrderRequest orderRequest)
    {
        var order = new Order()
        {
            Id = Guid.NewGuid(),
            ProductId = orderRequest.ProductId,
            Quantity = orderRequest.Quantity,
            CustomerName = orderRequest.CustomerName,
            CreatedOn = DateTime.UtcNow
        };

        _logger.LogInformation("order created at {date}", DateTime.Now.ToString("O"));

        return order;
    }

    public async Task SetStatusToSuccessfullyAsync(Stock stock)
    {
        //do some order status set to completed successfully operations and get model
        var order = new Order()
        {
            Id = Guid.NewGuid(),
            ProductId = stock.ProductId,
            Quantity = stock.Quantity,
            CustomerName = stock.CustomerName,
            CreatedOn = DateTime.UtcNow
        };

        _messageProducer.Publish(order, "order.completedsuccessfully");

        _logger.LogInformation("order status is set to completed successfully at {date}", DateTime.Now.ToString("O"));
    }

    public async Task SetStatusToUnsuccessfulAsync(Stock stock)
    {
        _logger.LogInformation("order status is set to unsuccessful at {date}", DateTime.Now.ToString("O"));
    }
}