using Bank.Clients.Api.Domain;
using Bank.Core.Messages.Integration;
using Bank.Message;

namespace Bank.Clients.Api.Services;
public class CreditCardCreatedConsumer :
    MessageConsumer<CreditCardCreatedEvent>
{
    private readonly IClientRepository _clientRepository;

    public CreditCardCreatedConsumer(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async override Task ConsumeMessage(ConsumedMessage<CreditCardCreatedEvent> message)
    {
        var client = await _clientRepository.GetById(message.Data.ClientId);
        
        if(client == null) 
            throw new MessageConsumedException("[ProposalRefusedConsumer.ConsumeMessage] - Não existe cliente cadastrado.");
        
        client.SetProposalApproved(message.Data.Message, message.Data.CreditLimit);
        
        _clientRepository.Update(client!);
       
        await _clientRepository.UnitOfWork.CommitAsync();
    }
}