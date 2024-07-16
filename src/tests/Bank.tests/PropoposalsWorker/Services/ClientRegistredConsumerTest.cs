using Bank.Core.Messages.Integration;
using Bank.Message;
using Bank.Proposals.Worker.Domain.Entities;
using Bank.Proposals.Worker.Domain.Services.Interface;
using Bank.Proposals.Worker.Services;
using MassTransit;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Bank.tests.PropoposalsWorker.Services;
public class ClientRegistredConsumerTest
{
    #region Mocks
    private readonly Mock<IMessageBus> _messageBusMock;
    private readonly Mock<IProposalService> _proposalServiceMock;
    private readonly AutoMocker _mocker = new();
    #endregion

    private readonly ClientRegistredEvent _event = new ClientRegistredEvent(
        Guid.NewGuid(),
        20000,
        "12345678");

    public ClientRegistredConsumerTest()
    {
        _proposalServiceMock = _mocker.GetMock<IProposalService>();
        _messageBusMock = _mocker.GetMock<IMessageBus>();
    }

    [Fact]
    public async Task Consume_WhenCalled_ShouldCreateCreditCard()
    {
        var consumer = _mocker.CreateInstance<ClientRegistredConsumer>();  

        var mockConsumeContext = new Mock<ConsumeContext<ClientRegistredEvent>>();
        mockConsumeContext.Setup(x => x.Message).Returns(_event);
        mockConsumeContext.Setup(x => x.ReceiveContext.Redelivered).Returns(false);
        
        var consumedMessage = 
            new ConsumedMessage<ClientRegistredEvent>(mockConsumeContext.Object);

        await consumer.ConsumeMessage(consumedMessage);

        _proposalServiceMock.Verify(x => x.ProccessProposal(It.IsAny<Proposal>()), Times.Once);
    }

}