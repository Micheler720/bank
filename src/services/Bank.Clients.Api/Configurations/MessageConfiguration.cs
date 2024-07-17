using System.Diagnostics.CodeAnalysis;
using Bank.Clients.Api.Services;
using Bank.Core.Messages.Integration;
using Bank.Message;

namespace Bank.Clients.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class MessageConfiguration
{
    public static void AddMessageConfig(
        this IServiceCollection services)
    {
        var consumers = new List<ConsumerConfiguration>
        {
            new ("proposal-refused-queue", typeof(ProposalRefusedConsumer)),
            new ("proposal-failed-queue", typeof(ProposalFailedConsumer)),
            new ("credit-card-created-queue", typeof(CreditCardCreatedConsumer)),
            new ("credit-card-refused-queue", typeof(CreditCardRefusedConsumer))
        };

        var producers = new List<ProducerConfiguration>
        {
            new("client-registred-queue", typeof(ClientRegistredEvent))
        };

        services.AddMessageBus(producerConfigurations: producers, consumerConfigurations: consumers);
        
    }
}