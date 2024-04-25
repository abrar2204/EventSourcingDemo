using EventSourcingDemo.Entities;
using EventSourcingDemo.Event;

namespace EventSourcingDemo.Domain;

public class PokemonAggregate : AggregateRoot
{
    private string _name;
    private int _level;
    private List<string> _type = [];
    private List<string> _attacks = [];
    
    public PokemonAggregate()
    {
    }

    public PokemonAggregate(Guid id, string name, int level, List<string> type, List<string> attacks)
    {
        ApplyChanges(new CapturePokemonEvent
        {
            Id = id,
            Name = name,
            Level = level,
            Type = type,
            Attacks = attacks
        });
    }

    public void Evolve(string name, int level,List<string> @type)
    {
        ApplyChanges(new EvolvePokemonEvent
        {
            Id = Id,
            Name = name,
            Level = level,
            Type = type
        });
    }

    public void LearnAttack(string attack)
    {
        ApplyChanges(new LearnAttackEvent
        {
            Id = Id,
            Attack = attack
        });
    }
    
    public void Apply(CapturePokemonEvent @event)
    {
        Id = @event.Id;
        _name = @event.Name;
        _level = @event.Level;
        _type = @event.Type;
        _attacks = @event.Attacks;
    }
    
    public void Apply(EvolvePokemonEvent @event)
    {
        Id = @event.Id;
        _name = @event.Name;
        _level = @event.Level;
        _type = @event.Type;
    }
    
    public void Apply(LearnAttackEvent @event)
    {
        Id = @event.Id;
        _attacks.Add(@event.Attack);
    }
}