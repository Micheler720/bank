using MassTransit;

namespace Bank.Message;

public class ConsumedMessage<T> where T : class
{
    public T Data { get; private set; }
    public bool Redelivered { get; private set; }
    public int RetryCount { get; private set; }

    public ConsumedMessage(T data, bool redelivered = false, int retryCount = 0)
    {
        Data = data;
        Redelivered = redelivered;
        RetryCount = retryCount;
    }
}