using Bank.Core.DomainObjects;

namespace Bank.Clients.Api.Domain;

public class Client : Entity
{
    public string? Name { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string? Document { get; private set; }
    public string? Email { get; private set; }
    public ProposalStatus ProposalStatus { get; private set; }
    public string? Observation { get; private set; }
    public decimal CreditLimit { get; private set; }

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

    public void SetProposalPending()
        => ProposalStatus = ProposalStatus.Pending;
    
    public void SetProposalRefused(string observation)
    {
        ProposalStatus = ProposalStatus.Refused;
        Observation = observation;
    }
    
    public void SetProposalApproved(string observation, decimal creditLimit)
    {
        ProposalStatus = ProposalStatus.Approved;
        CreditLimit = creditLimit;
        Observation = observation;
    }

    public void SetProposalFailedProposal(string observation)
    {
        ProposalStatus = ProposalStatus.FailedProposal;
        Observation = observation;
    }
}