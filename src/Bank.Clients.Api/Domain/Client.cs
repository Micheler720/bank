using Bank.Core.DomainObjects;

namespace Bank.Clients.Api.Domain;

public class Client : Entity
{
    public string? Name { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string? Document { get; private set; }
    public string? Email { get; private set; }

    protected Client()
    {
        
    }

    public Client(string name, DateTime birthDate, string document, string email)
    {
        Name = name;
        BirthDate = birthDate;
        Document = document;
        Email = email;
    }
}