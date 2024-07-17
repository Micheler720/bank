namespace Bank.CreditCard.Worker.Domain;

public class CreditCardEntity 
{
    public Guid Id { get; set; }
    public decimal CreditLimit { get; set; }
    public string CardNumber { get; set; }
    public string SecurityCode { get; set; }

    public CreditCardEntity(Guid id, decimal creditLimit, string cardNumber, string securityCode)
    {
        Id = id;
        CreditLimit = creditLimit;
        CardNumber = cardNumber;
        SecurityCode = securityCode;
    }

}