namespace Bank.Core.Messages.Integration;

public class CreditCardCreatedEvent : Event
{
    public Guid ClientId { get; set; }
    public string Message { get; set; }
    public decimal CreditLimit { get; set; } 

    public CreditCardCreatedEvent(
        Guid clientId, 
        string message,
        decimal creditLimit)
    {
        AggregateId = clientId;
        ClientId = clientId;
        CreditLimit = creditLimit;
        Message = message;
    }
}

