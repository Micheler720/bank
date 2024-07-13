using Bank.Core.Data;

namespace Bank.Clients.Api.Domain;

public interface IClientRepository : IRepository
{
    void Add(Client client);

}