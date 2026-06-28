using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Movie.Core;
using Movie_.Contracts;
using Movie_.Core;
using Movie_.Core.ModelDto;

// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
/// <summary>
/// Controller för att hantera CRUD-operationer för skådespelare (Actors).
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ActorsController : ControllerBase
{
    private readonly IActorService _actorService;

    public ActorsController(IActorService actorService)
    {
        _actorService = actorService;
    }

    // GET: api/actors
    /// <summary>
    /// Hämtar en lista över alla skådespelare.
    /// </summary>
    /// <returns>Status och en lista över skådespelare.</returns>
    [HttpGet]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public async Task<ActionResult<ICollection<ActorDto>>> GetActors()
    {
        var actors = await _actorService.GetActors();
        if (actors == null || !actors.Any())
        {
            return NotFound();
        }
        return Ok(actors);
    }

    //    GET: api/actor/5
    /// <summary>
    /// Hämtar en specifik skådespelare baserat på dess ID.
    /// </summary>
    /// <param name="actorid">ID för skådespelaren som ska hämtas.</param>
    /// <returns>Status och den specifika skådespelaren.</returns>
    [HttpGet("{actorid}")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public async Task<ActionResult<ActorDto>> GetActor(int actorid)
    {
        var actor = await _actorService.GetActor(actorid);
        if (actor == null)
        {
            return NotFound();
        }
        return Ok(actor);
    }

    // POST: api/actor
    /// <summary>
    /// Skapar en ny skådespelare.
    /// </summary>
    /// <param name="actor">Skådespelaren som ska skapas.</param>
    /// <returns>Status och den skapade skådespelaren.</returns>
    [HttpPost]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public async Task<ActionResult<ActorDto>> PostActor(ActorCreateDto actor)
    {
        var result = await _actorService.PostActor(actor);
        return CreatedAtAction(nameof(GetActor), new { actorId = result.Value?.ActorId }, result.Value);
    }

    // PUT: api/actor/5
    /// <summary>
    /// Uppdaterar en befintlig skådespelare baserat på dess ID.
    /// </summary>
    /// <param name="actorid">ID för skådespelaren som ska uppdateras.</param>
    /// <param name="actor">Skådespelaren med uppdaterad information.</param>
    /// <returns>Status för uppdateringsoperationen.</returns>
    [HttpPut("{actorid}")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public async Task<IActionResult> PutActor(int actorid, ActorUpdateDto actor)
    {
        return await _actorService.PutActor(actorid, actor);
    }

    // DELETE: api/actor/5
    /// <summary>
    /// Tar bort en skådespelare baserat på dess ID.
    /// </summary>
    /// <param name="actorid">ID för skådespelaren som ska tas bort.</param>
    /// <returns>Status för borttagningsoperationen.</returns>
    [HttpDelete("{actorid}")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public async Task<IActionResult> DeleteActor(int actorid)
    {
        var result = await _actorService.DeleteActor(actorid);
        return result;
    }
}
