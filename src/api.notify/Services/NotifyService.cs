namespace api.notify;

public class NotifyService : INotifyService
{
    private readonly ILogger<NotifyService> _logger;

    public NotifyService(ILogger<NotifyService> logger)
    {
        _logger = logger;
    }

    public async Task OrderCompletedSuccessfullyAsync(Order order)
    {
        _logger.LogInformation("{customerName} notified for the order completed successfully at {date}. OrderId:{orderId},ProductId:{productId}"
            , order.CustomerName, DateTime.Now.ToString("O"), order.Id, order.ProductId);
    }
}