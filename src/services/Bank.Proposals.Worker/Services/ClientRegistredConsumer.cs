using Bank.Core.Messages.Integration;
using Bank.Message;
using Bank.Proposals.Worker.Domain.Entities;
using Bank.Proposals.Worker.Domain.Services.Interface;
using Serilog;

namespace Bank.Proposals.Worker.Services;
public class ClientRegistredConsumer :
    MessageConsumer<ClientRegistredEvent>
{
    private readonly IMessageBus _messageBus;
    private readonly IProposalService _proposalService;

    public ClientRegistredConsumer(
        IMessageBus messageBus,
        IProposalService proposalService)
    {
        _messageBus = messageBus;
        _proposalService = proposalService;
    }

    public override async Task ConsumeMessage(ConsumedMessage<ClientRegistredEvent> message)
    {
        if (message.RetryCount == 5)
        {
            Log.Error("[ClientRegistredConsumer.ConsumeMessage] - Houve uma falha no consumo da proposta de crédito.");
            await _messageBus.Publish(new ProposalFailedEvent(message.Data.ClientId, "Houve uma falha no consumo da proposta de crédito."));
            return;
        }

        var proposal = new Proposal
        {
            ClientId = message.Data.ClientId,
            Document = message.Data.Document,
            SolicitedLimit = message.Data.SolicitedLimit
        };

        await _proposalService.ProccessProposal(proposal);
    }
}