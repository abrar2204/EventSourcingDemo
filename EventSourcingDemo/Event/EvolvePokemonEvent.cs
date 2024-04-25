namespace EventSourcingDemo.Event;

public class EvolvePokemonEvent : BaseEvent
{
    public EvolvePokemonEvent() :  base(nameof(EvolvePokemonEvent))
    {
    }

    public string Name { get; set; }
    public int Level { get; set; }
    public List<string> Type { get; set; }
}