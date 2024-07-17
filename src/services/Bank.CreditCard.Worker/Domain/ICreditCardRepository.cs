namespace Bank.CreditCard.Worker.Domain;
public interface ICreditCardRepository
{
    void Add(CreditCardEntity creditCard);
    void AddRange(IEnumerable<CreditCardEntity> creditCards);
    Task<IEnumerable<CreditCardEntity>?> GetByClient(Guid clientId);
}