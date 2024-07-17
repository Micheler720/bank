using Bank.Clients.Api.Domain;

namespace Bank.Clients.Api.Application.DTO;
public class ClientDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Document { get; set; }
    public string? Email { get; set; }
    public ProposalStatus ProposalStatus { get; set; }
    public string? Observation { get; set; }
    public decimal CreditLimit { get; set; }

    public ClientDto() { }

    public ClientDto(Client client)
    {
        Id = client.Id;
        Name = client.Name;
        BirthDate = client.BirthDate;
        Document = client.Document;
        Email = client.Email;
        ProposalStatus = client.ProposalStatus;
        Observation = client.Observation;
        CreditLimit = client.CreditLimit;
    }

}