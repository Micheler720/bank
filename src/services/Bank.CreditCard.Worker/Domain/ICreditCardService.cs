namespace Bank.CreditCard.Worker.Domain;

public interface ICreditCardService
{
    Task CreateCreditCard(Guid clientId, string document, decimal approvedLimit);
}
