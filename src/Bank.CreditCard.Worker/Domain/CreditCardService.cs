using Bank.Core.Messages.Integration;
using Bank.Message;

namespace Bank.CreditCard.Worker.Domain;

public class CreditCardService : ICreditCardService
{
    private readonly IMessageBus _messageBus;

    public CreditCardService(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public async Task CreateCreditCard(Guid clientId, string document, decimal approvedLimit)
    {
        var raizDocument = document[..2];

        if(raizDocument == "12")
        {
            await _messageBus.Publish(new CreditCardRefusedEvent(clientId, "Houve uma falha na criação do cartão de crédito"));
            return;
        }

        var numberCreditCard = Guid.NewGuid().ToString().Substring(0, 16);
        var securityCode = new Random().Next(100, 999).ToString();

        var creditCarCreatedEvent = new CreditCardCreatedEvent(
            clientId, 
            message: "Limite crédito aprovado.",
            creditLimit: approvedLimit,
            cardNumber: numberCreditCard,
            securityCode: securityCode,
            creditCardId: Guid.NewGuid());

        await _messageBus.Publish(creditCarCreatedEvent);
    }

}