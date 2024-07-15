namespace Bank.Core.Messages.Integration;

public class CreditCardRefusedEvent : Event 
{
    public Guid ClientId { get; private set; }
    public string Message { get; private set; }

    public CreditCardRefusedEvent(Guid clientId, string message)
    {
        AggregateId = clientId;
        ClientId = clientId;
        Message = message;
    }
}