using EventSourcingDemo.Persistence;
using EventSourcingDemo.Requests;
using EventSourcingDemo.Service;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcingDemo.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class PokemonController : ControllerBase
{
    private readonly PokemonService _pokemonService;

    public PokemonController(PokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    [HttpGet("/{id}")]
    public IActionResult GetPokemon([FromRoute] Guid id)
    {
        return Ok(_pokemonService.GetPokemon(id));
    }

    [HttpPost("/capture")]
    public IActionResult CapturePokemon([FromBody] CapturePokemonRequest request)
    {
       return Ok(_pokemonService.CapturePokemon(request));
    }
    
    [HttpPut("/evolve")]
    public void Evolve([FromBody] EvolvePokemonRequest request)
    {
        _pokemonService.EvolvePokemon(request);
    }
    
    [HttpPatch("/learn-attack")]
    public void LearnAttack([FromBody] LearnAttackRequest request)
    {
        _pokemonService.LearnAttack(request);
    }
}