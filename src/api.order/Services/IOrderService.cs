namespace api.order;

public interface IOrderService
{
    Task<Order> CreateAsync(OrderRequest orderRequest);

    Task SetStatusToSuccessfullyAsync(Stock stock);

    Task SetStatusToUnsuccessfulAsync(Stock stock);
}