using System.Text.Json.Serialization;
using Bank.Core.Messages;

namespace Bank.Core.DomainObjects;
public class Entity
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }  

    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    private List<Event>? _events;
    
    [JsonIgnore]
    public IReadOnlyCollection<Event>? Notifications => _events?.AsReadOnly();

    public void AddEvent(Event @event)
    {
        _events ??= new List<Event>();
        _events.Add(@event);
    }

    public void RemoveEvent(Event eventItem)
    {
        _events?.Remove(eventItem);
    }

    public void ClearEvents()
    {
        _events?.Clear();
    }
}