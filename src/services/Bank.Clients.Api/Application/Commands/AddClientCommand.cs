using Bank.Core.Messages;
using FluentValidation;

namespace Bank.Clients.Api.Application.Commands;

public class AddClientCommand : Command
{
    public string? Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Document { get; set; }
    public string? Email { get; set; }
    public decimal SolicitedLimit { get; set; }

    public override bool IsValid()
    {
        ValidationResult = new AddClientValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class AddClientValidation : AbstractValidator<AddClientCommand>
{
    public AddClientValidation()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Nome é obrigatório.");

        RuleFor(c => c.BirthDate)
            .NotEmpty()
            .WithMessage("Data Nascimento é obrigatório.");
        
        RuleFor(c => c.Document)
            .NotEmpty()
            .WithMessage("Documento é obrigatório.");
        
        RuleFor(c => c.Email)
            .NotNull()
            .WithMessage("E-mail é obrigatório.")
            .EmailAddress()
            .WithMessage("E-mail inválido.");

        RuleFor(c => c.SolicitedLimit)
            .NotEmpty()
            .WithMessage("Limite solicitado é obrigatório.");
    }
}