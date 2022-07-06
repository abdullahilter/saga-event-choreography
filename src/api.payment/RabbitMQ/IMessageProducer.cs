namespace api.payment;

public interface IMessageProducer
{
    void Publish<T>(T message, string queue);
}