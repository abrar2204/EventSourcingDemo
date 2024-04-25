using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventSourcingDemo.Entities;

[Table("Pokemons")]
public class Pokemon
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public virtual ICollection<PokemonType> Types { get; set; }
    public virtual ICollection<PokemonAttack> Attacks { get; set; }
}