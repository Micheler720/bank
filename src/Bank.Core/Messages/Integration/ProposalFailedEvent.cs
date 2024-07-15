namespace Bank.Core.Messages.Integration;

public class ProposalFailedEvent : Event
{
    public Guid ClientId { get; private set; }
    public string Message { get; private set; }

    public ProposalFailedEvent(Guid clientId, string message)
    {
        AggregateId = clientId;
        ClientId = clientId;
        Message = message;
    }

}