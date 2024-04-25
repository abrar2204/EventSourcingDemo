using EventSourcingDemo.Entities;
using EventSourcingDemo.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EventSourcingDemo.Event;

public class EventHandler
{
    private readonly EventSourcingDemoDbContext _db;

    public EventHandler(EventSourcingDemoDbContext db)
    {
        _db = db;
    }
    
    private void On(CapturePokemonEvent @event)
    {
        var pokemon = new Pokemon
        {
            Id = @event.Id.ToString(),
            Name = @event.Name,
            Level = @event.Level,
            Types = new List<PokemonType>(),
            Attacks = new List<PokemonAttack>()
        };
        
        foreach (var type in @event.Type)
        {
            var pokemonType = _db.PokemonType.FirstOrDefault(t => type == t.Name) ?? new PokemonType
            {
                Id = Guid.NewGuid().ToString(),
                Name = type
            };
            _db.PokemonType.Add(pokemonType);
            pokemon.Types.Add(pokemonType);
        }
        
        foreach (var attack in @event.Attacks)
        {
            var pokemonAttack = _db.PokemonAttack.FirstOrDefault(a => attack == a.Name) ?? new PokemonAttack
            {
                Id = Guid.NewGuid().ToString(),
                Name = attack
            };
            _db.PokemonAttack.Add(pokemonAttack);
            pokemon.Attacks.Add(pokemonAttack);
        }

        _db.Pokemon.Add(pokemon);

        _db.SaveChanges();
    }
    
    private void On(EvolvePokemonEvent @event)
    {
        var pokemon = _db.Pokemon.Find(@event.Id.ToString());
        if (pokemon is null)
        {
            throw new ArgumentNullException();
        }
        _db.Entry(pokemon).Collection(p => p.Types).Load(); 

        pokemon.Name = @event.Name;
        pokemon.Level = @event.Level;
        pokemon.Types.Clear();
        foreach (var type in @event.Type)
        {
            var pokemonType = _db.PokemonType.FirstOrDefault(t => type == t.Name);
            if (pokemonType is null)
            {
                pokemonType = new PokemonType
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = type
                };
                _db.PokemonType.Add(pokemonType);
            }
            pokemon.Types.Add(pokemonType);
        }

        _db.Pokemon.Update(pokemon);
        _db.SaveChanges();
    }
    
    private void On(LearnAttackEvent @event)
    {
        var pokemon = _db.Pokemon.Find(@event.Id.ToString());
        if (pokemon is null)
        {
            throw new ArgumentNullException();
        }
        _db.Entry(pokemon).Collection(p => p.Attacks).Load(); 
        
        var pokemonAttack = _db.PokemonAttack.FirstOrDefault(t => @event.Attack == t.Name);
        if (pokemonAttack is null)
        {
            pokemonAttack = new PokemonAttack
            {
                Id = Guid.NewGuid().ToString(),
                Name = @event.Attack
            };
            _db.PokemonAttack.Add(pokemonAttack);
        }
        pokemon.Attacks.Add(pokemonAttack);
        
        _db.Pokemon.Update(pokemon);
        _db.SaveChanges();
    }

    public void Handle(BaseEvent @event)
    {
        switch (@event.Type)
        {
            case nameof(CapturePokemonEvent):
                On((CapturePokemonEvent)@event);
                break;
            case nameof(EvolvePokemonEvent):
                On((EvolvePokemonEvent)@event);
                break;
            case nameof(LearnAttackEvent):
                On((LearnAttackEvent)@event);
                break;
            default:
                throw new NotSupportedException();
        }
    }
}