namespace Bank.Message;
public class MessageConsumedException : Exception
{
    public MessageConsumedException()
    {

    }
    public MessageConsumedException(string message) : base(message)
    {
    }

    
}