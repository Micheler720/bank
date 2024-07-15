namespace Bank.Core.Messages.Integration;

public class CreditCardCreatedEvent : Event
{
    public Guid CreditCardId { get; set; }
    public Guid ClientId { get; set; }
    public string Message { get; set; }
    public decimal CreditLimit { get; set; }
    public string CardNumber { get; set; }
    public string SecurityCode { get; set; }

    public CreditCardCreatedEvent(
        Guid clientId, 
        string message,
        decimal creditLimit,
        string cardNumber,
        string securityCode,
        Guid creditCardId)
    {
        AggregateId = clientId;
        ClientId = clientId;
        Message = message;
        CreditLimit = creditLimit;
        CardNumber = cardNumber;
        SecurityCode = securityCode;
        CreditCardId = creditCardId;
    }
}