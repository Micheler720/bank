using System.Diagnostics.CodeAnalysis;

namespace Bank.Message;

[ExcludeFromCodeCoverage]
public class ProducerConfiguration
{
    public string QueueName { get; set; }
    public Type ProducerType { get; set; }

    public ProducerConfiguration(string queueName, Type producerType)
    {
        QueueName = queueName;
        ProducerType = producerType;
    }

}