using Bank.Core.Messages.Integration;
using Bank.Message;
using Bank.Proposals.Worker.Domain.Entities;
using Bank.Proposals.Worker.Domain.HttpService;
using Bank.Proposals.Worker.Domain.Services;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Bank.tests.PropoposalsWorker.Domain.Services;

public class ProposalServiceTest
{
    #region Mocks
    private readonly Mock<IScoreHttpService> _scoreHttpServiceMock;
    private readonly Mock<IMessageBus> _messageBusMock;
    private readonly AutoMocker _mocker = new();
    #endregion

    private readonly Proposal _proposal = new()
    {
        Document = "12345678901",
        ClientId = Guid.NewGuid(),
        SolicitedLimit = 5000
    };

    public ProposalServiceTest()
    {
        _scoreHttpServiceMock = _mocker.GetMock<IScoreHttpService>();
        _messageBusMock = _mocker.GetMock<IMessageBus>();
    }

    [Fact]
    public async Task Should_PublishProposalApproved()
    {
        _scoreHttpServiceMock.Setup(x => x.GetScore(It.IsAny<string>()))
            .ReturnsAsync(301);

        var service = _mocker.CreateInstance<ProposalService>();

        await service.ProccessProposal(_proposal);

        _messageBusMock.Verify(x => x.Publish(
            It.IsAny<ProposalApprovedEvent>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_PublishProposalRefused()
    {
        _scoreHttpServiceMock.Setup(x => x.GetScore(It.IsAny<string>()))
            .ReturnsAsync(300);

        var service = _mocker.CreateInstance<ProposalService>();

        await service.ProccessProposal(_proposal);

        _messageBusMock.Verify(x => x.Publish(
            It.IsAny<ProposalRefusedEvent>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }
    
}