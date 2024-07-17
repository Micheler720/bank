using Bank.Core.Messages.Integration;
using Bank.CreditCard.Worker.Domain;
using Bank.CreditCard.Worker.Services;
using Bank.Message;
using MassTransit;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Bank.tests.CreditCardWorker.Services;
public class ProposalApprovedConsumerTest
{

    #region Mocks
    private readonly Mock<IMessageBus> _messageBusMock;
    private readonly Mock<ICreditCardService> _creditCardServiceMock;
    private readonly AutoMocker _mocker = new();
    #endregion
    
    private readonly ProposalApprovedEvent _event = new ProposalApprovedEvent(
        "12345678",
        Guid.NewGuid(),
        20000);

    public ProposalApprovedConsumerTest()
    {
        _creditCardServiceMock = _mocker.GetMock<ICreditCardService>();        
        _messageBusMock = _mocker.GetMock<IMessageBus>();  
    }

    [Fact]
    public async Task ConsumeMessage_WhenCalled_ShouldProposalAproved()
    {
        var consumer = _mocker.CreateInstance<ProposalApprovedConsumer>();  

        var mockConsumeContext = new Mock<ConsumeContext<ProposalApprovedEvent>>();
        mockConsumeContext.Setup(x => x.Message).Returns(_event);
        mockConsumeContext.Setup(x => x.ReceiveContext.Redelivered).Returns(false);
        
        var consumedMessage = 
            new ConsumedMessage<ProposalApprovedEvent>(mockConsumeContext.Object);

        await consumer.ConsumeMessage(consumedMessage);

        _creditCardServiceMock.Verify(x => x.CreateCreditCard(
            It.IsAny<Guid>(), 
            It.IsAny<string>(), 
            It.IsAny<List<decimal>>()), Times.Once);

        _messageBusMock.Verify(x => x.Publish(
            It.IsAny<ProposalFailedEvent>(), 
            It.IsAny<CancellationToken>()), Times.Never);
    }

}