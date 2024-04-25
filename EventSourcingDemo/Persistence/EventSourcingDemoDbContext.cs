using EventSourcingDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventSourcingDemo.Persistence;

public class EventSourcingDemoDbContext : DbContext
{
    public EventSourcingDemoDbContext(DbContextOptions<EventSourcingDemoDbContext> options)
        : base(options)
    {
    }

    public DbSet<EventStore> EventStore { get; set; }
    public DbSet<Pokemon> Pokemon { get; set; }
    public DbSet<PokemonType> PokemonType { get; set; }
    public DbSet<PokemonAttack> PokemonAttack { get; set; }
}