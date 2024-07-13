namespace Bank.Core.Messages.Integration;

public class ClientRegistredEvent : Event
{
    public string? Name { get; private set; }
    public Guid Id { get; private set; }
    public float CreditLimit { get; private set; }
    
    public ClientRegistredEvent(string name, Guid id, float creditLimit)
    {
        AggregateId = id;
        Id = id;
        Name = name;
        CreditLimit = creditLimit;
    }
}
