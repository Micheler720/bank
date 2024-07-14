using System.Diagnostics.CodeAnalysis;
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

        services.AddMessageBus(consumers);
        
    }
}