namespace shared;

public interface IMessageProducer
{
    void Publish<T>(T message, string queue);
}