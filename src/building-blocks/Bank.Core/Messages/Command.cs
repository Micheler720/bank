using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using FluentValidation.Results;
using MediatR;

namespace Bank.Core.Messages;

[ExcludeFromCodeCoverage]
public class Command : Message, IRequest<ValidationResult>
{
    [JsonIgnore]
    public DateTime Timestamp { get; private set; }
    
    [JsonIgnore]
    public ValidationResult? ValidationResult { get; set; }

    protected Command()
    {
        Timestamp = DateTime.Now;
    }

    public virtual bool IsValid()
    {
        throw new NotImplementedException();
    }
}