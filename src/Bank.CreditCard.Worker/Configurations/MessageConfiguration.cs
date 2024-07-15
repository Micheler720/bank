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
            new() {
                QueueName = "proposal-approved-queue",
                ConsumerType = typeof(ProposalApprovedConsumer)
            }
        };

        var producers = new List<ProducerConfiguration>
        {
            new() {
                QueueName = "credit-card-refused-queue",
                ProducerType = typeof(CreditCardRefusedEvent)
            },
            new() {
                QueueName = "credit-card-created-queue",
                ProducerType = typeof(CreditCardCreatedEvent)
            },
        };

        services.AddMessageBus(producerConfigurations: producers, consumerConfigurations: consumers);
        
    }
}