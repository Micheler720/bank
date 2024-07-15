namespace Bank.Clients.Api.Domain
{
    public enum ProposalStatus
    {
        Pending = 1,
        FailedProposal = 2,
        FailedCreditCard = 3,
        Refused = 4,
        Approved =  5,
    }
}