using Bank.Core.Messages.Integration;
using Bank.CreditCard.Worker.Exceptions;
using Bank.Message;

namespace Bank.CreditCard.Worker.Domain;

public class CreditCardService : ICreditCardService
{
    private readonly IMessageBus _messageBus;
    private readonly ICreditCardRepository _creditCardRepository;

    public CreditCardService(
        IMessageBus messageBus,
        ICreditCardRepository creditCardRepository)
    {
        _messageBus = messageBus;
        _creditCardRepository = creditCardRepository;
    }

    public async Task CreateCreditCard(Guid clientId, string document, IEnumerable<decimal> aprovedsLimits)
    {
        var raizDocument = document[..2];

        if (raizDocument == "12")
        {
            await _messageBus.Publish(new CreditCardRefusedEvent(clientId, "Houve uma falha na criação do cartão de crédito"));
            return;
        }

        var creditCards = aprovedsLimits.Select(limit =>
        {
            var numberCreditCard = Guid.NewGuid().ToString().Substring(0, 16);
            var securityCode = new Random().Next(100, 999).ToString();

            return new CreditCardEntity(
                clientId: clientId,
                creditLimit: limit,
                cardNumber: numberCreditCard,
                securityCode: securityCode);
        }).ToList();

        _creditCardRepository.AddRange(creditCards);

        var success = await _creditCardRepository.UnitOfWork.CommitAsync();

        if(!success)
            throw new PersistDataException("Houve uma falha na persistencia dos cartões de crédito");        

        var creditCarCreatedEvent = new CreditCardCreatedEvent(
            clientId,
            message: "Limite crédito aprovado.",
            creditLimit: aprovedsLimits.Sum());

        await _messageBus.Publish(creditCarCreatedEvent);
    }

}