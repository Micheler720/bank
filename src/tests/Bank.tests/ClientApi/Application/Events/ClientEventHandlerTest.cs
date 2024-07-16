using Bank.Clients.Api.Application.Events;
using Bank.Core.Messages.Integration;
using Bank.Message;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Bank.tests.ClientApi.Application.Events;
public class ClientEventHandlerTest
{
    #region Mocks
    private readonly Mock<IMessageBus> _messageBusMock;
    private readonly AutoMocker _mocker = new();
    #endregion

    public ClientEventHandlerTest()
    {
        _messageBusMock = _mocker.GetMock<IMessageBus>();
    }

    [Fact]
    public async Task Handle_should_publish_notification()
    {
        var notificationHandler = _mocker.CreateInstance<ClientEventHandler>();

        var clienteRegistredEvent = new ClientRegistredEvent(Guid.NewGuid(), 2000, "12345678");

        await notificationHandler.Handle(clienteRegistredEvent, default);

        _messageBusMock.Verify(v => v.Publish(
            It.IsAny<ClientRegistredEvent>(),
            It.IsAny<CancellationToken>()), 
            Times.Once);
    }

}