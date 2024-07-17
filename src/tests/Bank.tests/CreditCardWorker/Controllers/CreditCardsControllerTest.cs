using System.Net;
using Bank.Core.Data;
using Bank.CreditCard.Worker.Controllers.v1;
using Bank.CreditCard.Worker.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Bank.tests.ClientApi.Controllers;
public class CreditCardsControllerTest
{
    #region Mocks
    private readonly Mock<ICreditCardRepository> _creditCardRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly AutoMocker _mocker = new();
    #endregion

    private readonly CreditCardEntity _creditCard = new CreditCardEntity(
        clientId: Guid.NewGuid(),
        creditLimit: 2000,
        cardNumber: "123456789",
        securityCode: "123"
    );

    public CreditCardsControllerTest()
    {
        _unitOfWorkMock = _mocker.GetMock<IUnitOfWork>();
        _unitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(true);
        _creditCardRepositoryMock = _mocker.GetMock<ICreditCardRepository>();

        _creditCardRepositoryMock.SetupGet(x => x.UnitOfWork).Returns(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Get_ShouldGetCredtCard()
    {
        var clientId = Guid.NewGuid();

        _creditCardRepositoryMock.Setup(x => x.GetByClientId(It.IsAny<Guid>()))
            .ReturnsAsync([_creditCard]);
        
        var controller = _mocker.CreateInstance<CreditCardsController>();

        var result = await controller.Get(clientId) as ObjectResult;
        var creditCards = result?.Value as IEnumerable<CreditCardEntity>;

        Assert.Equal((int)HttpStatusCode.OK, result!.StatusCode);
        Assert.Equal(_creditCard, creditCards!.First());

        _creditCardRepositoryMock.Verify(x => x.GetByClientId(clientId), Times.Once);
    }

    [Fact]
    public async Task Get_ShouldGet_NoContent()
    {
        var clientId = Guid.NewGuid();        

        _creditCardRepositoryMock.Setup(x => x.GetByClientId(clientId))
            .ReturnsAsync((List<CreditCardEntity>)null!);
        
        var controller = _mocker.CreateInstance<CreditCardsController>();

        var result = await controller.Get(clientId) as NoContentResult;

        Assert.Equal((int)HttpStatusCode.NoContent, result!.StatusCode);

        _creditCardRepositoryMock.Verify(x => x.GetByClientId(clientId), Times.Once);
    }
}