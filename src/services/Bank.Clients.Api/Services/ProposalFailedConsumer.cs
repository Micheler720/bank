using Bank.Clients.Api.Domain;
using Bank.Core.Messages.Integration;
using Bank.Message;

namespace Bank.Clients.Api.Services;

public class ProposalFailedConsumer : MessageConsumer<ProposalFailedEvent>
{
    private readonly IClientRepository _clientRepository;

    public ProposalFailedConsumer(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async override Task ConsumeMessage(ConsumedMessage<ProposalFailedEvent> message)
    {
        var client = await _clientRepository.GetById(message.Data.ClientId);

        if (client == null)
            throw new MessageConsumedException("[ProposalRefusedConsumer.ConsumeMessage] - Não existe cliente cadastrado.");

        client.SetProposalFailedProposal(message.Data.Message);

        _clientRepository.Update(client!);

        await _clientRepository.UnitOfWork.CommitAsync();
    }
}