using Bank.Clients.Api.Application.DTO;
using Bank.Clients.Api.Domain;

namespace Bank.Clients.Api.Application.Queries;

public interface IClientQuery
{
    public Task<ClientDto?> GetByDocument(string document);
}

public class ClientQuery : IClientQuery
{
    private readonly IClientRepository _clientRepository;

    public ClientQuery(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<ClientDto?> GetByDocument(string document)
    {
        var client = await _clientRepository.GetByDocument(document);
        
        if(client == null) return null;

        return new ClientDto(client);
    }
}