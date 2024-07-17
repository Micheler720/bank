using System.Diagnostics.CodeAnalysis;
using MassTransit;

namespace Bank.Message
{
    [ExcludeFromCodeCoverage]
    public abstract class MessageConsumer<T> : IConsumer<T> where T : class
    {
        public abstract Task ConsumeMessage(ConsumedMessage<T> message);
        public async Task Consume(ConsumeContext<T> message)
        {
            var redelivered = message.ReceiveContext.Redelivered;
            var retryCount = message.GetRetryCount() + 1;

            var consumedMessage = new ConsumedMessage<T>(
                message.Message, 
                redelivered, 
                retryCount);

            await ConsumeMessage(consumedMessage);
        }
    }
}