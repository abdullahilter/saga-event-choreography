namespace api.stock;

public interface IStockService
{
    Task<bool> ReduceAsync(Payment payment);
}