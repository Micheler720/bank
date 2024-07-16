using Bank.Core.Messages.Integration;
using Bank.CreditCard.Worker.Domain;
using Bank.Message;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Bank.tests.CreditCardWorker.Domain;

public class CreditCardServiceTest
{
    #region Mocks
    private readonly Mock<IMessageBus> _messageBusMock;
    private readonly AutoMocker _mocker = new();
    #endregion

    public CreditCardServiceTest()
    {
        _messageBusMock = _mocker.GetMock<IMessageBus>();
    }

    [Fact]
    public async Task CreateCreditCard_ShouldCreateCreditCard()
    {
        var creditCardService = _mocker.CreateInstance<CreditCardService>();
        
        await creditCardService.CreateCreditCard(Guid.NewGuid(), "15345678", 20000);

        _messageBusMock.Verify(x => x.Publish(
            It.IsAny<CreditCardCreatedEvent>(), 
            It.IsAny<CancellationToken>()), Times.Once);

        _messageBusMock.Verify(x => x.Publish(
            It.IsAny<CreditCardRefusedEvent>(), 
            It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CreateCreditCard_ShouldRefusedCreditCard()
    {
        var creditCardService = _mocker.CreateInstance<CreditCardService>();
        
        await creditCardService.CreateCreditCard(Guid.NewGuid(), "12345678", 20000);

        _messageBusMock.Verify(x => x.Publish(
            It.IsAny<CreditCardCreatedEvent>(), 
            It.IsAny<CancellationToken>()), Times.Never);

        _messageBusMock.Verify(x => x.Publish(
            It.IsAny<CreditCardRefusedEvent>(), 
            It.IsAny<CancellationToken>()), Times.Once);
    }
}