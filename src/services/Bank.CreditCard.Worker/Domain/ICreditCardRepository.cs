using Bank.Core.Data;

namespace Bank.CreditCard.Worker.Domain;

public interface ICreditCardRepository : IRepository
{
    void Add(CreditCardEntity creditCard);
    void AddRange(IEnumerable<CreditCardEntity> creditCards);
    Task<IEnumerable<CreditCardEntity>?> GetByClientId(Guid clientId);
}