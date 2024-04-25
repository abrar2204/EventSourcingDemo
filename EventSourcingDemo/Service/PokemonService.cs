using EventSourcingDemo.Domain;
using EventSourcingDemo.Entities;
using EventSourcingDemo.Repository;
using EventSourcingDemo.Requests;

namespace EventSourcingDemo.Service;

public class PokemonService
{
    private readonly PokemonRepository _pokemonRepository;
    private readonly PokemonReadRepository _pokemonReadRepository;

    public PokemonService(PokemonRepository pokemonRepository, PokemonReadRepository pokemonReadRepository)
    {
        _pokemonRepository = pokemonRepository;
        _pokemonReadRepository = pokemonReadRepository;
    }

    public string CapturePokemon(CapturePokemonRequest request)
    {
        PokemonAggregate pokemon = new PokemonAggregate(Guid.NewGuid(), request.Name, request.Level, request.Type, request.Attacks);
        _pokemonRepository.Save(pokemon);
        return pokemon.Id.ToString();
    }

    public void EvolvePokemon(EvolvePokemonRequest request)
    {
        PokemonAggregate pokemon = _pokemonRepository.GetPokemon(request.Id);
        pokemon.Evolve(request.Name, request.Level, request.Type);
        _pokemonRepository.Save(pokemon);
    }

    public void LearnAttack(LearnAttackRequest request)
    {
        PokemonAggregate pokemon = _pokemonRepository.GetPokemon(request.Id);
        pokemon.LearnAttack(request.Attack);
        _pokemonRepository.Save(pokemon);
    }

    public Pokemon GetPokemon(Guid id)
    {
        return _pokemonReadRepository.GetPokemon(id);
    }
}