using EventSourcingDemo.Event;

namespace EventSourcingDemo.Domain;

public abstract class AggregateRoot
{
    private readonly List<BaseEvent> _changes = new List<BaseEvent>();
    public Guid Id { get; protected set; }
    public int Version { get; set; } = -1;

    public IEnumerable<BaseEvent> GetChanges()
    {
        return _changes;
    }
    
    protected void ApplyChanges(BaseEvent @event)
    {
        var method = this.GetType().GetMethod("Apply", new Type[] { @event.GetType() });
        method?.Invoke(this, new object[]{@event});
        _changes.Add(@event);
    }
    
    public void ReplayEvents(IEnumerable<BaseEvent> events)
    {
        foreach (var @event in events)
        {
            var method = this.GetType().GetMethod("Apply", new Type[] { @event.GetType() });
            method?.Invoke(this, new object[]{@event});
        }
    }
}