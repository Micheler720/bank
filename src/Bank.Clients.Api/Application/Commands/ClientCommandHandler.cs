using Bank.Core.Mediator;
using FluentValidation.Results;
using MediatR;

namespace Bank.Clients.Api.Application.Commands;
public class ClientCommandHandler(IMediator mediator) : MediatorHandler(mediator),
    IRequestHandler<AddClientCommand, ValidationResult>
{
    public async Task<ValidationResult> Handle(AddClientCommand request, CancellationToken cancellationToken)
    {
        if(!request.IsValid()) return request.ValidationResult;   

        return new ValidationResult();  
    }
}