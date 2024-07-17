using Bank.Core.DomainObjects;

namespace Bank.Proposals.Worker.Domain.Entities;

public class Proposal : Entity
{
    public int Score { get; set; }
    public string Document { get; set; }
    public Guid ClientId { get; set; }
    public decimal SolicitedLimit { get; set; }
}