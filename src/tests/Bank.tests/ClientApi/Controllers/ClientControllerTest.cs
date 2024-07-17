using System.Net;
using Bank.Clients.Api.Application.Commands;
using Bank.Clients.Api.Application.DTO;
using Bank.Clients.Api.Application.Queries;
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
    private readonly Mock<IClientQuery> _clientQueryMock;
    private readonly AutoMocker _mocker = new();
    #endregion

    private readonly ClientDto _clientDto = new ClientDto()
    {
        Document = "123456789",
        Name = "John Doe",
        Email = "john.doe@example.com",
        Observation = "Client observation"
    };

    public ClientControllerTest()
    {
        _mediatorHandlerMock = _mocker.GetMock<IMediatorHandler>();
        _clientQueryMock = _mocker.GetMock<IClientQuery>();

        _mediatorHandlerMock.Setup(m => m.SendCommand(It.IsAny<AddClientCommand>()))
            .ReturnsAsync(new ValidationResult());
    }

    [Fact]
    public async Task Add_ShouldAddClient()
    {
        var client = new AddClientCommand()
        {
            Document = "123456789",
            Name = "John Doe",
            Email = "john.doe@example.com",
        };
        
        var controller = _mocker.CreateInstance<ClientController>();

        var result = await controller.Add(client) as ObjectResult;

        Assert.Equal((int)HttpStatusCode.Created, result!.StatusCode);

        _mediatorHandlerMock.Verify(x => x.SendCommand(client), Times.Once);
    }

    [Fact]
    public async Task Get_ShouldGetClient()
    {
        var document = "123456789";        

        _clientQueryMock.Setup(x => x.GetByDocument(document))
            .ReturnsAsync(_clientDto);
        
        var controller = _mocker.CreateInstance<ClientController>();

        var result = await controller.Get(document) as ObjectResult;
        var client = result?.Value as ClientDto;

        Assert.Equal((int)HttpStatusCode.OK, result!.StatusCode);
        Assert.Equal(_clientDto.Observation, client!.Observation);
        Assert.Equal(_clientDto.Name, client.Name);
        Assert.Equal(_clientDto.BirthDate, client.BirthDate);
        Assert.Equal(_clientDto.Document, client.Document);
        
        _clientQueryMock.Verify(x => x.GetByDocument(document), Times.Once);
    }

    [Fact]
    public async Task Get_ShouldGet_NoContent()
    {
        var document = "123456789";        

        _clientQueryMock.Setup(x => x.GetByDocument(document))
            .ReturnsAsync((ClientDto)null!);
        
        var controller = _mocker.CreateInstance<ClientController>();

        var result = await controller.Get(document) as NoContentResult;

        Assert.Equal((int)HttpStatusCode.NoContent, result!.StatusCode);

        _clientQueryMock.Verify(x => x.GetByDocument(document), Times.Once);
    }
}