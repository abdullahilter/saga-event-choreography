namespace api.notify;

public interface IMessageProducer
{
    void Publish<T>(T message, string queue);
}