using Bank.Core.Messages;
using FluentValidation;

namespace Bank.Clients.Api.Application.Commands;

public class AddClientCommand : Command
{
    public string? Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Document { get; set; }
    public string? Email { get; set; }
    public float CreditLimit { get; set; }
    public float SolicitedLimit { get; set; }

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
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(c => c.BirthDate).NotEmpty().WithMessage("BirthDate is required");
        RuleFor(c => c.Document).NotEmpty().WithMessage("Document is required");
        RuleFor(c => c.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(c => c.CreditLimit).NotEmpty().WithMessage("CreditLimit is required");
    }
}