namespace Bank.Message;

public class ConsumerConfiguration
{
    public string QueueName { get; set; }
    public Type ConsumerType { get; set; }
    public int CountRetry { get; set; } = 5;
}