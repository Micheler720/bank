namespace Bank.Message;

public interface IMessageBus
{
    Task Publish<T>(T message, CancellationToken cancellationToken = default);
}