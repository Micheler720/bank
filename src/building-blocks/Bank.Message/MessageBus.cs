using System.Diagnostics.CodeAnalysis;
using MassTransit;

namespace Bank.Message;

[ExcludeFromCodeCoverage]
public class MessageBus(IPublishEndpoint publishEndpoint) : IMessageBus
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    
    public async Task Publish<T>(T message, CancellationToken cancellationToken = default)
    {
        await _publishEndpoint.Publish(message, cancellationToken);
    }
}