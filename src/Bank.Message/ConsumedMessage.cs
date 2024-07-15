using MassTransit;

namespace Bank.Message;

public class ConsumedMessage<T> where T : class
{
    public T Data { get; private set; }
    public bool Redelivered { get; private set; }
    public int RetryCount { get; private set; }

    public ConsumedMessage(ConsumeContext<T> message)
    {
        Data = message.Message;
        Redelivered = message.ReceiveContext.Redelivered;
        RetryCount = message.GetRetryCount() + 1;
    }
}