using Bank.Core.Messages.Integration;
using Bank.Message;
using Bank.Proposals.Worker.Domain.Entities;
using Bank.Proposals.Worker.Domain.HttpService;
using Bank.Proposals.Worker.Domain.Services.Interface;

namespace Bank.Proposals.Worker.Domain.Services;

public class ProposalService : IProposalService
{
    private readonly IScoreHttpService _scoreHttpService;
    private readonly IMessageBus _messageBus;

    public ProposalService(
        IScoreHttpService scoreHttpService,
        IMessageBus messageBus)
    {
        _scoreHttpService = scoreHttpService;
        _messageBus = messageBus;
    }

    public async Task ProccessProposal(Proposal proposal)
    {
       proposal.Score = await _scoreHttpService.GetScore(proposal.Document!);

       if(proposal.Score <= 300)
       {
            var proposalRefusedEvent = new ProposalRefusedEvent
            (
                proposal.Id,
                proposal.ClientId,
                "Proposta reprovada devido a score inadequado"
            );

            await _messageBus.Publish(proposalRefusedEvent);

            return;
       }

       var approvedLimits = new decimal[]
       {
            1000,
            2000,
            3000
       };

       var proposalApprovedEvent = new ProposalApprovedEvent
       (
           proposal.Document,
           proposal.ClientId,
           approvedLimits
       );

       await _messageBus.Publish(proposalApprovedEvent);

    }
}