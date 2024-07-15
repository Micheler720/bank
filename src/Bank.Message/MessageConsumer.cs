using MassTransit;

namespace Bank.Message
{
    public abstract class MessageConsumer<T> : IConsumer<T> where T : class
    {
        public abstract Task ConsumeMessage(ConsumedMessage<T> message);
        public async Task Consume(ConsumeContext<T> message)
        {
            var consumedMessage = new ConsumedMessage<T>(message);
            await ConsumeMessage(consumedMessage);
        }
    }
}