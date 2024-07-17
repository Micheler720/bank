using Bank.Core.Data;

namespace Bank.Clients.Api.Domain;

public interface IClientRepository : IRepository
{
    void Add(Client client);
    void Update(Client client);
    Task<Client?> GetById(Guid clientId);
    Task<Client?> GetByDocument(string document);
}