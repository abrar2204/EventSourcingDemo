using EventSourcingDemo.Domain;
using EventSourcingDemo.Entities;
using EventSourcingDemo.Event;
using EventSourcingDemo.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EventSourcingDemo.Repository;

public class PokemonReadRepository
{
    private readonly EventSourcingDemoDbContext _db;

    public PokemonReadRepository(EventSourcingDemoDbContext db)
    {
        _db = db;
    }
    
    public Pokemon GetPokemon(Guid id)
    {
        return _db.Pokemon
            .Include(p => p.Types)
            .Include(p => p.Attacks)
            .First(p => p.Id == id.ToString());
    }
}