using System.Diagnostics.CodeAnalysis;

namespace Bank.Message;

[ExcludeFromCodeCoverage]
public class ProducerConfiguration
{
    public string? QueueName { get; set; }
    public Type? ProducerType { get; set; }

}