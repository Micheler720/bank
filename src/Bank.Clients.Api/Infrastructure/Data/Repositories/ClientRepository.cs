using Bank.Clients.Api.Domain;

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
}