namespace Bank.Core.Messages.Integration;

public class ProposalRefusedEvent : Event
{
    public Guid ProposalId { get; private set; }
    public Guid ClientId { get; private set; }
    public string Message { get; private set; }

    public ProposalRefusedEvent(Guid proposalId, Guid clientId, string message)
    {
        AggregateId = proposalId;
        ProposalId = proposalId;
        ClientId = clientId;
        Message = message;
    }
}