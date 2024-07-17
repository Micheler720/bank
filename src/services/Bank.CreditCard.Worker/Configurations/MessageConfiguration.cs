using System.Diagnostics.CodeAnalysis;
using Bank.Core.Messages.Integration;
using Bank.CreditCard.Worker.Services;
using Bank.Message;

namespace Bank.CreditCard.Worker.Configurations;

[ExcludeFromCodeCoverage]
public static class MessageConfiguration
{
    public static void AddMessageConfig(
        this IServiceCollection services)
    {
        var consumers = new List<ConsumerConfiguration>
        {
            new("proposal-approved-queue", typeof(ProposalApprovedConsumer))
        };

        var producers = new List<ProducerConfiguration>
        {
            new("credit-card-refused-queue", typeof(CreditCardRefusedEvent)),
            new("credit-card-created-queue", typeof(CreditCardCreatedEvent))
        };

        services.AddMessageBus(producerConfigurations: producers, consumerConfigurations: consumers);
        
    }
}