using System.Text.Json;
using EventSourcingDemo.Domain;
using EventSourcingDemo.Entities;
using EventSourcingDemo.Event;
using EventSourcingDemo.Persistence;
using Microsoft.EntityFrameworkCore;
using EventHandler = EventSourcingDemo.Event.EventHandler;

namespace EventSourcingDemo.Repository;

public class PokemonRepository
{
    private readonly EventSourcingDemoDbContext _db;
    private readonly EventHandler _eventHandler;
    
    public PokemonRepository(EventSourcingDemoDbContext db, EventHandler eventHandler)
    {
        _db = db;
        _eventHandler = eventHandler;
    }

    public void Save(PokemonAggregate pokemon)
    {
        int version = pokemon.Version;
        foreach (var @event in pokemon.GetChanges())
        {
            version++;
            @event.Version = version;
            _db.EventStore.Add(
                new EventStore
                {
                    Id = Guid.NewGuid().ToString(),
                    Version = version,
                    AggregateIdentifier = pokemon.Id,
                    EventType = @event.Type,
                    EventData = JsonSerializer.Serialize<BaseEvent>(@event),
                });
            
            // materialize our events into queryable data
            _eventHandler.Handle(@event);
        }
        _db.SaveChanges();
    }

    public PokemonAggregate GetPokemon(Guid requestId)
    {
        IEnumerable<EventStore> eventStores = _db.EventStore.Where(e => e.AggregateIdentifier == requestId);
        IEnumerable<BaseEvent> events = eventStores.Select(Deserialize);
        
        var pokemon = new PokemonAggregate();
        pokemon.ReplayEvents(events);
        pokemon.Version = events.Select(x => x.Version).Max();
        return pokemon;
    }
    
    private static BaseEvent Deserialize(EventStore eventStore)
    {
        switch (eventStore.EventType)
        {
            case nameof(CapturePokemonEvent):
                return JsonSerializer.Deserialize<CapturePokemonEvent>(eventStore.EventData);
            case nameof(EvolvePokemonEvent):
                return JsonSerializer.Deserialize<EvolvePokemonEvent>(eventStore.EventData);
            case nameof(LearnAttackEvent):
                return JsonSerializer.Deserialize<LearnAttackEvent>(eventStore.EventData);
            default:
                throw new NotSupportedException();
        }
    }
}