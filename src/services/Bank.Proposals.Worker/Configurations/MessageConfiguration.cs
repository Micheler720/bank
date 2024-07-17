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
            new("client-registred-queue", typeof(ClientRegistredConsumer))
        };

        var producers = new List<ProducerConfiguration>
        {
            new("proposal-refused-queue", typeof(ProposalRefusedEvent)),
            new("proposal-approved-queue", typeof(ProposalApprovedEvent)),
            new("proposal-failed-queue", typeof(ProposalFailedEvent))
        };

        services.AddMessageBus(producerConfigurations: producers, consumerConfigurations: consumers);
        
    }
}