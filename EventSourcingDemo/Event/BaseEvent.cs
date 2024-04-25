using System.Text.Json.Serialization;

namespace EventSourcingDemo.Event;

[JsonDerivedType(typeof(CapturePokemonEvent), nameof(CapturePokemonEvent))]
[JsonDerivedType(typeof(EvolvePokemonEvent), nameof(EvolvePokemonEvent))]
[JsonDerivedType(typeof(LearnAttackEvent), nameof(LearnAttackEvent))]
public class BaseEvent
{
    public Guid Id { get; set; }
    
    protected BaseEvent(string type)
    {
        this.Type = type;
    }
    
    public int Version { get; set; }
    public string Type { get; set; }
}