using Bank.Clients.Api.Domain;
using Bank.Clients.Api.Services;
using Bank.Core.Data;
using Bank.Core.Messages.Integration;
using Bank.Message;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Bank.tests.ClientApi.Services;

public class CreditCardCreatedConsumerTest
{
    #region Mocks
    private readonly Mock<IClientRepository> _clientRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly AutoMocker _mocker = new();
    #endregion

    private readonly Client _client = new Client(
        name: "Name",
        birthDate: new DateTime(1999, 10, 10),
        document: "12345678910",
        email: "XXXXXXXXXXXXXXX");
    
    private readonly CreditCardCreatedEvent _creditCardCreatedEvent = new CreditCardCreatedEvent(
        Guid.NewGuid(),
        "Aproved",
        20000);

    public CreditCardCreatedConsumerTest()
    {
        _unitOfWorkMock = _mocker.GetMock<IUnitOfWork>();
        _unitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(true);
        
        _clientRepositoryMock = _mocker.GetMock<IClientRepository>();  
        _clientRepositoryMock.SetupGet(x => x.UnitOfWork).Returns(_unitOfWorkMock.Object);      

        _clientRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(_client);
    }

    [Fact]
    public async Task Consume_WhenCalled_ShouldCreateCreditCard()
    {
        var consumer = _mocker.CreateInstance<CreditCardCreatedConsumer>();
        
        var consumedMessage = 
            new ConsumedMessage<CreditCardCreatedEvent>(_creditCardCreatedEvent);

        await consumer.ConsumeMessage(consumedMessage);

        _clientRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        _clientRepositoryMock.Verify(x => x.Update(It.IsAny<Client>()), Times.Once);
        _clientRepositoryMock.Verify(x => x.Update(It.Is<Client>(c => c.ProposalStatus == ProposalStatus.Approved)), Times.Once);
        _clientRepositoryMock.Verify(x => x.Update(It.Is<Client>(c => c.CreditLimit == 20000)), Times.Once);
    }

    [Fact]
    public async Task Consume_ShouldThrowException_WhenClientNotRegistred()
    {
        _clientRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync((Client)null!);

        var consumer = _mocker.CreateInstance<CreditCardCreatedConsumer>();
        
        var consumedMessage = 
            new ConsumedMessage<CreditCardCreatedEvent>(_creditCardCreatedEvent);

        var ex = await Assert.ThrowsAsync<MessageConsumedException>(() => consumer.ConsumeMessage(consumedMessage));

        _clientRepositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        _clientRepositoryMock.Verify(x => x.Update(It.IsAny<Client>()), Times.Never);
    }

}