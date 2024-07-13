using Bank.Core.Messages.Integration;
using MassTransit;
using Serilog;

namespace Bank.Proposals.Worker.Services
{
    public class ClientRegistredConsumer :
        IConsumer<ClientRegistredEvent>
    {
        public Task Consume(ConsumeContext<ClientRegistredEvent> context)
        {
           Log.Error("testando");
           return Task.CompletedTask;
        }
    }
}