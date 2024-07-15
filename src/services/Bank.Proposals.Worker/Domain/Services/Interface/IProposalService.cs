using Bank.Proposals.Worker.Domain.Entities;

namespace Bank.Proposals.Worker.Domain.Services.Interface;

public interface IProposalService
{
    Task ProccessProposal(Proposal proposal);
}
