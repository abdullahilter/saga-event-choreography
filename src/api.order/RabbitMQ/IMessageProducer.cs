namespace api.order;

public interface IMessageProducer
{
    void Publish<T>(T message, string queue);
}