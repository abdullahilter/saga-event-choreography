namespace api.notify;

public interface INotifyService
{
    Task OrderCompletedSuccessfullyAsync(Order order);
}