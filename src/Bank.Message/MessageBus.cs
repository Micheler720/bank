using MassTransit;

namespace Bank.Message;

public class MessageBus(IBusControl publishEndpoint) : IMessageBus
{
    private readonly IBusControl _publishEndpoint = publishEndpoint;
    public async Task Publish<T>(T message, CancellationToken cancellationToken = default)
    {
        await _publishEndpoint.Publish(message, cancellationToken);
    }
}