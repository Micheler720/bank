namespace Bank.Core.Messages.Integration;

public class ProposalApprovedEvent : Event
{
    public string Document { get; private set; }
    public Guid ClientId { get; private set; }
    public decimal[] ApprovedLimits { get; private set; }

    public ProposalApprovedEvent(string document, Guid clientId, params decimal[] approvedLimits)
    {
        AggregateId = clientId;
        Document = document;
        ClientId = clientId;
        ApprovedLimits = approvedLimits;
    }

}
