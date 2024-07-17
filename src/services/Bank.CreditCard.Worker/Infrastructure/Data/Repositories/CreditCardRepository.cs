using System.Diagnostics.CodeAnalysis;
using Bank.CreditCard.Worker.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bank.CreditCard.Worker.Infrastructure.Data.Repositories;

[ExcludeFromCodeCoverage]
public class CreditCardRepository : Repository, ICreditCardRepository
{
    public CreditCardRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public void Add(CreditCardEntity creditCard)
    {
        _context.CreditCards.Add(creditCard);
    }
    public void AddRange(IEnumerable<CreditCardEntity> creditCards)
    {
        _context.CreditCards.AddRange(creditCards);
    }

    public async Task<IEnumerable<CreditCardEntity>?> GetByClientId(Guid clientId)
    {
        return await _context.CreditCards.Where(creditCard => creditCard.ClientId == clientId)
            .ToListAsync();
    }
}