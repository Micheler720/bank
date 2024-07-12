using Bank.Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace Bank.Core.Mediator;
public class MediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ValidationResult> SendCommand<T>(T comando) where T : Command
    {
        return await _mediator.Send(comando);
    }

}