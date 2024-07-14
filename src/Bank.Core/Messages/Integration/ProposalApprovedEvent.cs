namespace Bank.Core.Messages.Integration;

public class ProposalApprovedEvent : Event
{
    public Guid ProposalId { get; private set; }
    public Guid ClientId { get; private set; }
    public decimal SolicitedLimit { get; private set; }

    public ProposalApprovedEvent(Guid proposalId, Guid clientId, decimal salicitedLimit)
    {
        ProposalId = proposalId;
        ClientId = clientId;
        SolicitedLimit = salicitedLimit;
    }

}