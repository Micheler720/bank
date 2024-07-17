using Bank.Clients.Api.Application.Queries;
using Bank.Clients.Api.Domain;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Bank.tests.ClientApi.Application.Queries;

public class ClientQueryTest
{
    #region Mocks
    private readonly Mock<IClientRepository> _clientRepoisitoryMock;
    private readonly AutoMocker _mocker = new();
    #endregion

    private readonly Client _client = new Client(
        name: "Name",
        birthDate: new DateTime(1999, 10, 10),
        document: "12345678910",
        email: "XXXXXXXXXXXXXXX");

    public ClientQueryTest()
    {
        _clientRepoisitoryMock = _mocker.GetMock<IClientRepository>();
    }
    
    [Fact]
    public async Task GetByDocument_ShouldGetClient()
    {
        var query = _mocker.CreateInstance<ClientQuery>();

        _client.SetProposalApproved("Approved", 20000);

        _clientRepoisitoryMock.Setup(x => x.GetByDocument(It.IsAny<string>()))
            .ReturnsAsync(_client);

        var client = await query.GetByDocument("123456789");

        Assert.Equal(_client.Name, client!.Name);
        Assert.Equal(_client.BirthDate, client.BirthDate);
        Assert.Equal(_client.Document, client.Document);
        Assert.Equal(_client.Email, client.Email);
        Assert.Equal(_client.ProposalStatus, client.ProposalStatus);
        Assert.Equal(_client.Observation, client.Observation);
    }

    [Fact]
    public async Task GetByDocument_ShouldNull()
    {
        var query = _mocker.CreateInstance<ClientQuery>();

        _clientRepoisitoryMock.Setup(x => x.GetByDocument(It.IsAny<string>()))
            .ReturnsAsync((Client)null!);

        var client = await query.GetByDocument("123456789");

        Assert.Null(client);
    }

}