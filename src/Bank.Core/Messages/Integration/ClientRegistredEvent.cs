namespace Bank.Core.Messages.Integration;

public class ClientRegistredEvent : Event
{
    public Guid ClientId { get; private set; }
    public float SolicitedLimit { get; private set; }
    public string Document { get; private set; }
    
    public ClientRegistredEvent(Guid clientId, float solicitedLimit, string document)
    {
        AggregateId = clientId;
        ClientId = clientId;
        SolicitedLimit = solicitedLimit;
        Document = document;
    }
}
