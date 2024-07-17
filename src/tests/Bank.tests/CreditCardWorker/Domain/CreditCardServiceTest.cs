using Bank.Core.Data;
using Bank.Core.Messages.Integration;
using Bank.CreditCard.Worker.Domain;
using Bank.CreditCard.Worker.Exceptions;
using Bank.Message;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Bank.tests.CreditCardWorker.Domain;

public class CreditCardServiceTest
{
    #region Mocks
    private readonly Mock<IMessageBus> _messageBusMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ICreditCardRepository> _creditCardRepositoryMock;
    private readonly AutoMocker _mocker = new();
    #endregion

    private readonly List<decimal> _approvedLimits = new() { 20000 };

    public CreditCardServiceTest()
    {
        _unitOfWorkMock = _mocker.GetMock<IUnitOfWork>();
        _unitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(true);

        _messageBusMock = _mocker.GetMock<IMessageBus>();
        _creditCardRepositoryMock = _mocker.GetMock<ICreditCardRepository>();
        _creditCardRepositoryMock.SetupGet(x => x.UnitOfWork).Returns(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task CreateCreditCard_ShouldCreateCreditCard()
    {
        var creditCardService = _mocker.CreateInstance<CreditCardService>();
        
        await creditCardService.CreateCreditCard(Guid.NewGuid(), "15345678", _approvedLimits);

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
        
        await creditCardService.CreateCreditCard(Guid.NewGuid(), "12345678", _approvedLimits);

        _messageBusMock.Verify(x => x.Publish(
            It.IsAny<CreditCardCreatedEvent>(), 
            It.IsAny<CancellationToken>()), Times.Never);

        _messageBusMock.Verify(x => x.Publish(
            It.IsAny<CreditCardRefusedEvent>(), 
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateCreditCard_ShouldException_WhenErroPersistData()
    {
        var creditCardService = _mocker.CreateInstance<CreditCardService>();
       
        _creditCardRepositoryMock.Setup(x => x.UnitOfWork.CommitAsync())
            .ReturnsAsync(false);
        
        await Assert.ThrowsAsync<PersistDataException>(() =>
            creditCardService.CreateCreditCard(Guid.NewGuid(), "15345678", _approvedLimits)); 

        _messageBusMock.Verify(x => x.Publish(
            It.IsAny<CreditCardCreatedEvent>(), 
            It.IsAny<CancellationToken>()), Times.Never);

        _messageBusMock.Verify(x => x.Publish(
            It.IsAny<CreditCardRefusedEvent>(), 
            It.IsAny<CancellationToken>()), Times.Never);
    }
}