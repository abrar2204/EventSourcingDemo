namespace EventSourcingDemo.Event;

public class CapturePokemonEvent : BaseEvent
{
    public CapturePokemonEvent() : base(nameof(CapturePokemonEvent))
    { }
    
    public string Name { get; set; }
    public int Level { get; set; }
    public List<string> Type { get; set; }
    public List<string> Attacks { get; set; }
}