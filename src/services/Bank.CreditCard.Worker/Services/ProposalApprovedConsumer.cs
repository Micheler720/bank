using Bank.Core.Messages.Integration;
using Bank.CreditCard.Worker.Domain;
using Bank.Message;

namespace Bank.CreditCard.Worker.Services;
public class ProposalApprovedConsumer :
    MessageConsumer<ProposalApprovedEvent>
{
    private readonly IMessageBus _messageBus;
    private readonly ICreditCardService _creditCardService;

    public ProposalApprovedConsumer(IMessageBus messageBus, ICreditCardService creditCardService)
    {
        _messageBus = messageBus;
        _creditCardService = creditCardService;
    }

    public async override Task ConsumeMessage(ConsumedMessage<ProposalApprovedEvent> message)
    {
        if(message.RetryCount == 5)
        {
            await _messageBus.Publish(new ProposalFailedEvent(message.Data.ClientId, "Houve uma falha na geração do Cartão de crédito"));
            return;
        }

        await _creditCardService.CreateCreditCard(
            message.Data.ClientId, 
            message.Data.Document, 
            message.Data.ApprovedLimits);

    }
}