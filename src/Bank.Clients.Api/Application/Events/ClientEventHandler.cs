using Bank.Core.Messages.Integration;
using MassTransit;
using MediatR;

namespace Bank.Clients.Api.Application.Events;
public class ClientEventHandler(IBusControl publishEndpoint) : 
    INotificationHandler<ClientRegistredEvent>
{
    private readonly IBusControl _publishEndpoint = publishEndpoint;

    public async Task Handle(
        ClientRegistredEvent notification, 
        CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish(notification, cancellationToken);
    }
}