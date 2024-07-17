using Bank.Clients.Api.Application.Commands;
using Bank.Clients.Api.Domain;
using Bank.Core.Data;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Bank.tests.ClientApi.Application.Commands;

public class ClientCommandHandlerTest
{
    #region Mocks
    private readonly Mock<IClientRepository> _clientRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly AutoMocker _mocker = new();
    #endregion

    private readonly AddClientCommand _command = new()
    {
        Name = "Teste",
        BirthDate = DateTime.Today.AddYears(-18),
        Email = "teste@teste.com",
        Document = "12345678910",
        SolicitedLimit =  20000
    };

    private readonly Client _client = new Client(
        name: "Name",
        birthDate: new DateTime(1999, 10, 10),
        document: "12345678910",
        email: "XXXXXXXXXXXXXXX");

    public ClientCommandHandlerTest()
    {
        _unitOfWorkMock = _mocker.GetMock<IUnitOfWork>();
        _unitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(true);
        _clientRepositoryMock = _mocker.GetMock<IClientRepository>(); 
        _clientRepositoryMock.SetupGet(x => x.UnitOfWork).Returns(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_should_set_proposal_pendind_when_register_client()
    {
        var handler = _mocker.CreateInstance<ClientCommandHandler>();

        var result = await handler.Handle(_command, CancellationToken.None);
        
        Assert.True(result.IsValid);

        _clientRepositoryMock.Verify(x => x.Add(It.Is<Client>(c => c.Notifications!.Count == 1)), Times.Once);
        _clientRepositoryMock.Verify(x => x.Add(It.Is<Client>(c => c.ProposalStatus == ProposalStatus.Pending)), Times.Once);
    }

    [Fact]
    public async Task Handle_should_invalid_when_command_invalid()
    {
        var handler = _mocker.CreateInstance<ClientCommandHandler>();
        _command.Email = "invalid";

        var result = await handler.Handle(_command, CancellationToken.None);
        
        Assert.False(result.IsValid);

        _clientRepositoryMock.Verify(x => x.Add(It.IsAny<Client>()), Times.Never);
    }

    [Fact]
    public async Task Handle_should_invalid_when_client_registred()
    {
        var handler = _mocker.CreateInstance<ClientCommandHandler>();
        _clientRepositoryMock.Setup(x => 
            x.GetByDocument(It.IsAny<string>())).ReturnsAsync(_client);
        
        var result = await handler.Handle(_command, CancellationToken.None);
        
        var errorMessage = result.Errors.First().ErrorMessage;
        
        Assert.False(result.IsValid);
        Assert.Equal("Documento já cadastrado.", errorMessage);

        _clientRepositoryMock.Verify(x => x.Add(It.IsAny<Client>()), Times.Never);
    }

}