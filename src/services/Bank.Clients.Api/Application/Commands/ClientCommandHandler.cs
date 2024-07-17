using Bank.Clients.Api.Domain;
using Bank.Core.Messages;
using Bank.Core.Messages.Integration;
using FluentValidation.Results;
using MediatR;

namespace Bank.Clients.Api.Application.Commands;

public class ClientCommandHandler(IClientRepository clientRepository) : CommandHandler,
    IRequestHandler<AddClientCommand, ValidationResult>
{
    private readonly IClientRepository _clientRepository = clientRepository;
    public async Task<ValidationResult> Handle(AddClientCommand request, CancellationToken cancellationToken)
    {
        if(!request.IsValid()) return request.ValidationResult!;   

        var client = await _clientRepository.GetByDocument(request.Document!);

        if (client != null)
        {
            AddError("Documento já cadastrado.");
            return ValidationResult;
        }

        client = new Client(
            name: request.Name!, 
            birthDate: request.BirthDate,
            document:request.Document!, 
            email: request.Email!);

        _clientRepository.Add(client);

        var clienteRegistred = new ClientRegistredEvent
        (
            clientId: client.Id,
            solicitedLimit: request.SolicitedLimit,
            document: request.Document!
        );

        client.SetProposalPending();
        client.AddEvent(clienteRegistred);

        return await PersistData(_clientRepository.UnitOfWork); 
    }
}