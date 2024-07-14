namespace Bank.Message;

public class ConsumerConfiguration
{
    public string QueueName { get; set; }
    public Type ConsumerType { get; set; }
}