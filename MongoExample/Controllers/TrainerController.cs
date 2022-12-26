
// AspNetCore.Mvc gives access to functions, types and http methods such as HttpGet, Put, Delete etc.
using Microsoft.AspNetCore.Mvc;
using MongoExample.Services;
using MongoExample.Models;
// Swashbuckle annotations is used to give the action methods a better description of what they do. 
using Swashbuckle.AspNetCore.Annotations;

// A controller is in some cases referred to as the manager while the services are workers. It manages the incoming request
// and dispatches the work to a suitable worker (in this case trainerService)
namespace MongoExample.Controllers;

[Controller]
[Route("api/[controller]")]
[Produces("application/json")]
public class TrainerController : Controller
{

    private readonly TrainerService _trainerService;

    public TrainerController(TrainerService trainerService)
    {
        _trainerService = trainerService;
    }

    // Here we perform a HttpGet request, a Get request only returns values, it cannot have a body.

    [HttpGet]
    [SwaggerOperation(Summary = "Retrieves all trainers", Description = "Returns an array of trainers")]
    // Task<List<Trainer>> means the expected return type is the result of this task. TResult contains a list of trainers.
    public async Task<List<Trainer>> Get()
    {
        return await _trainerService.getTrainers();
    }

        /// <remarks>
/// Sample request:
///
///     GET /Todo
///     {
///        "id": 1,
///        "name": "Item #1",
///        "isComplete": true
///     }
///
/// </remarks>
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Retrieves trainer by id", Description = "Returns a single trainer")]
    // In this function we take in an id [FromRoute] which means id will contain the url path.
    public async Task<ActionResult<Trainer>> GetById([FromRoute] string id)
    {
        var trainer = await _trainerService.getTrainer(id);

        // if trainer is null then we return NotFound() which is a part of the AspNetCore.Mvc package. It produces a status code of 404. 
        if (trainer is null)
        {
            return NotFound();
        }

        // If trainer exists, we return the trainer.
        return trainer;
    }

    /// <remarks>
/// Sample request:
///
///     POST /Todo
///{
///  "name": "Trainer",
///  "region": "Kanto",
///  "pokemons": []
///}
///
/// </remarks>
    [HttpPost]
    // [FromBody] means trainer will contain the data provided in the body.
    public async Task<IActionResult> Post([FromBody] Trainer trainer)
    {
        await _trainerService.createTrainer(trainer);
        // CreatedAtAction returns a CreatedAtActionResult that contains our URL for retrieving this new trainer data. 
        // createdataction takes in an actionName to use for generating the url (Get), a routeValue to specify where to find the object
        // the routeValue is the trainer.Id in this case, lastly it takes in the value that should be returned when requesting the returned endpoint.
        return CreatedAtAction(nameof(Get), new { id = trainer.Id }, trainer);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Adds pokemon to selected trainer")]
    public async Task<IActionResult> Put(string id, [FromBody] string pokemonId)
    {
        await _trainerService.addPokemonToTrainer(id, pokemonId);
        return NoContent();
    }

    /// <summary>
    /// Deletes a specific
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _trainerService.deleteTrainer(id);
        return NoContent();
    }

}