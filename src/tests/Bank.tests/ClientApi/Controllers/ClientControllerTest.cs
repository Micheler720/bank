using System.Net;
using Bank.Clients.Api.Application.Commands;
using Bank.Clients.Api.Controllers.v1;
using Bank.Core.Mediator;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Bank.tests.ClientApi.Controllers;
public class ClientControllerTest
{
    #region Mocks
    private readonly Mock<IMediatorHandler> _mediatorHandlerMock;
    private readonly AutoMocker _mocker = new();
    #endregion

    public ClientControllerTest()
    {
        _mediatorHandlerMock = _mocker.GetMock<IMediatorHandler>();

        _mediatorHandlerMock.Setup(m => m.SendCommand(It.IsAny<AddClientCommand>()))
            .ReturnsAsync(new ValidationResult());
    }

    [Fact]
    public async Task Register_ShouldRegisterClient()
    {
        var client = new AddClientCommand()
        {
            Document = "123456789",
            Name = "John Doe",
            Email = "john.doe@example.com",
        };
        
        var controller = _mocker.CreateInstance<ClientController>();

        var result = await controller.Register(client) as ObjectResult;

        Assert.Equal((int)HttpStatusCode.Created, result!.StatusCode);

        _mediatorHandlerMock.Verify(x => x.SendCommand(client), Times.Once);
    }
}