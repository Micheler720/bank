using System.Diagnostics.CodeAnalysis;

namespace Bank.CreditCard.Worker.Exceptions;

[ExcludeFromCodeCoverage]
public class PersistDataException : Exception
{
    public PersistDataException(string message) : base(message)
    {
    }

    public PersistDataException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public PersistDataException()
    {
    }

}