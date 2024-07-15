using Bank.Clients.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bank.Clients.Api.Infrastructure.Data.Repositories;

public class ClientRepository : Repository, IClientRepository
{
    public ClientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public void Add(Client client)
    {
        _context.Clients.Add(client);
    }

    public void Update(Client client)
    {
        _context.Clients.Update(client);
    }

    public async Task<Client?> GetById(Guid clientId)
    {
        return await _context.Clients.Where(client => client.Id == clientId)
            .FirstOrDefaultAsync();
    }
}