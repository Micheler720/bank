using Bank.Core.Messages.Integration;
using Bank.Message;
using MediatR;

namespace Bank.Clients.Api.Application.Events;
public class ClientEventHandler(IMessageBus messageBus) : 
    INotificationHandler<ClientRegistredEvent>
{
    private readonly IMessageBus _messageBus = messageBus;

    public async Task Handle(
        ClientRegistredEvent notification, 
        CancellationToken cancellationToken)
    {
        await _messageBus.Publish(notification, cancellationToken);
    }
}