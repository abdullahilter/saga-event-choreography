namespace api.payment;

public interface IPaymentService
{
    Task<bool> GetMoneyAsync(Order order);

    Task RefundAsync(Stock stock);
}