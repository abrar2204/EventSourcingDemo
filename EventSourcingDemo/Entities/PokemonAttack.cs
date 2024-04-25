using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventSourcingDemo.Entities;

[Table("PokemonAttacks")]
public class PokemonAttack
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public virtual ICollection<Pokemon> Pokemons { get; set; }
}