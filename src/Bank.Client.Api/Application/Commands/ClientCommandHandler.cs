using Bank.Core.Mediator;
using FluentValidation.Results;
using MediatR;

namespace Bank.Client.Api.Application.Commands;
public class ClientCommandHandler(IMediator mediator) : MediatorHandler(mediator),
    IRequestHandler<AddClientCommand, ValidationResult>
{
    public Task<ValidationResult> Handle(AddClientCommand request, CancellationToken cancellationToken)
    {

        throw new NotImplementedException();
    }
}