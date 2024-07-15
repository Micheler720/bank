namespace Bank.Core.Messages.Integration;

public class ProposalApprovedEvent : Event
{
    public string Document { get; private set; }
    public Guid ClientId { get; private set; }
    public decimal ApprovedLimit { get; private set; }

    public ProposalApprovedEvent(string document, Guid clientId, decimal approvedLimit)
    {
        AggregateId = clientId;
        Document = document;
        ClientId = clientId;
        ApprovedLimit = approvedLimit;
    }

}