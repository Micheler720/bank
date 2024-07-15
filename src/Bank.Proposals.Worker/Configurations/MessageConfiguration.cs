using System.Diagnostics.CodeAnalysis;
using Bank.Core.Messages.Integration;
using Bank.Message;
using Bank.Proposals.Worker.Services;

namespace Bank.Proposals.Worker.Configurations;

[ExcludeFromCodeCoverage]
public static class MessageConfiguration
{
    public static void AddMessageConfig(
        this IServiceCollection services)
    {
        var consumers = new List<ConsumerConfiguration>
        {
            new() {
                QueueName = "client-registred-queue",
                ConsumerType = typeof(ClientRegistredConsumer)
            }
        };

        var producers = new List<ProducerConfiguration>
        {
            new() {
                QueueName = "proposal-refused-queue",
                ProducerType = typeof(ProposalRefusedEvent)
            },
            new() {
                QueueName = "proposal-approved-queue",
                ProducerType = typeof(ProposalApprovedEvent)
            },
            new() {
                QueueName = "proposal-failed-queue",
                ProducerType = typeof(ProposalFailedEvent)
            },

        };

        services.AddMessageBus(producerConfigurations: producers, consumerConfigurations: consumers);
        
    }
}