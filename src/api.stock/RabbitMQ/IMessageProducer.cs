namespace api.stock;

public interface IMessageProducer
{
    void Publish<T>(T message, string queue);
}