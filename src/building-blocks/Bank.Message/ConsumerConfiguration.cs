using System.Diagnostics.CodeAnalysis;

namespace Bank.Message;

[ExcludeFromCodeCoverage]
public class ConsumerConfiguration
{
    public string QueueName { get; set; }
    public Type ConsumerType { get; set; }
    public int CountRetry { get; set; } = 5;

    public ConsumerConfiguration(string queueName, Type consumerType, int countRetry = 5)
    {
        QueueName = queueName;
        ConsumerType = consumerType;
        CountRetry = countRetry;
    }
}