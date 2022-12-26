using System;
using Microsoft.AspNetCore.Mvc;
using MongoExample.Services;
using MongoExample.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace MongoExample.Controllers;

[Controller]
[Route("api/[controller]")]
[Produces("application/json")]
public class PokemonController : Controller
{

    private readonly PokemonService _pokemonService;

    public PokemonController(PokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }


    [HttpGet]
    [SwaggerOperation(Summary = "Retrieves all available pokemons", Description = "Returns an array of pokemons")]
    public async Task<List<Pokemon>> Get()
    {
        return await _pokemonService.getPokemons();
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Retrieves a pokemon by id", Description = "Returns a single pokemon")]
    public async Task<ActionResult<Pokemon>> GetById([FromRoute] string id)
    {
        var pokemon = await _pokemonService.getPokemon(id);

        if (pokemon is null)
        {
            return NotFound();
        }

        return pokemon;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Pokemon pokemon)
    {
        await _pokemonService.createPokemon(pokemon);
        return CreatedAtAction(nameof(Get), new { id = pokemon.Id }, pokemon);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Adds a move to selected pokemon")]
    public async Task<IActionResult> Put(string id, [FromBody] string move)
    {
        await _pokemonService.addMoveToPokemon(id, move);
        return NoContent();
    }

/// <summary>
    /// Deletes a specific pokemon
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _pokemonService.deletePokemon(id);
        return NoContent();
    }

}