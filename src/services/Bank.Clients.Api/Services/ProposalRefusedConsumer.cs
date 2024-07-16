using Bank.Clients.Api.Domain;
using Bank.Core.Messages.Integration;
using Bank.Message;

namespace Bank.Clients.Api.Services;
public class ProposalRefusedConsumer :
    MessageConsumer<ProposalRefusedEvent>
{
    private readonly IClientRepository _clientRepository;

    public ProposalRefusedConsumer(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async override Task ConsumeMessage(ConsumedMessage<ProposalRefusedEvent> message)
    {
        var client = await _clientRepository.GetById(message.Data.ClientId);
        
        if(client == null) 
            throw new MessageConsumedException("[ProposalRefusedConsumer.ConsumeMessage] - Não existe cliente cadastrado.");
        
        client.SetProposalRefused(message.Data.Message);
        _clientRepository.Update(client!);
       
        await _clientRepository.UnitOfWork.CommitAsync();
    }
}