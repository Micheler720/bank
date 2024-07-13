using Bank.Core.Data;
using FluentValidation.Results;

namespace Bank.Core.Messages;
public abstract class CommandHandler
{

    protected ValidationResult ValidationResult;

    protected CommandHandler()
    {
        ValidationResult = new ValidationResult();
    }

    protected void AddError(string message)
    {
        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
    }

    protected async Task<ValidationResult> PersistData(IUnitOfWork uow)
    {
        if (!await uow.CommitAsync()) AddError("An error occurred while trying to persist data");

        return ValidationResult;
    }

}