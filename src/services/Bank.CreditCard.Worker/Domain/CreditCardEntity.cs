using Bank.Core.DomainObjects;

namespace Bank.CreditCard.Worker.Domain;

public class CreditCardEntity : Entity
{
    public Guid ClientId { get; set; }
    public decimal CreditLimit { get; set; }
    public string CardNumber { get; set; }
    public string SecurityCode { get; set; }

    public CreditCardEntity(Guid clientId, decimal creditLimit, string cardNumber, string securityCode)
    {
        ClientId = clientId;
        CreditLimit = creditLimit;
        CardNumber = cardNumber;
        SecurityCode = securityCode;
    }

}