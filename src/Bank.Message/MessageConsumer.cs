using MassTransit;

namespace Bank.Message
{
    public abstract class MessageConsumer<T> : IConsumer<T> where T : class
    {
        public abstract Task ConsumeMessage(ConsumedMessage<T> context);
        public async Task Consume(ConsumeContext<T> context)
        {
            var consumedMessage = new ConsumedMessage<T>(context);
            await ConsumeMessage(consumedMessage);
        }
    }
}