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
            new() {
                QueueName = "proposal-refused-queue",
                ConsumerType = typeof(ProposalRefusedConsumer)
            },
            new() {
                QueueName = "credit-card-created-queue",
                ConsumerType = typeof(CreditCardConsumer)
            }
        };

        var producers = new List<ProducerConfiguration>
        {
            new() {
                QueueName = "client-registred-queue",
                ProducerType = typeof(ClientRegistredEvent)
            }
        };

        services.AddMessageBus(producerConfigurations: producers, consumerConfigs: consumers);
        
    }
}