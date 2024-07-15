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
        ValidationResult = new AddClientCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class AddClientCommandValidation : AbstractValidator<AddClientCommand>
{
    public AddClientCommandValidation()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name é obrigatório.");
        RuleFor(c => c.BirthDate).NotEmpty().WithMessage("BirthDate é obrigatório.");
        RuleFor(c => c.Document).NotEmpty().WithMessage("Document é obrigatório.");
        RuleFor(c => c.Email).NotEmpty().WithMessage("Email é obrigatório.");
        RuleFor(c => c.SolicitedLimit).NotEmpty().WithMessage("SolicitedLimit é obrigatório.");
    }
}