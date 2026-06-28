using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Movie_.Contracts;
using Movie_.Core;
using Movie_.Core.ModelDto;
using System.Data;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class MoviesController : ControllerBase
{
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    // GET: api/movies
    /// <summary>
    /// Hämtar en lista över alla filmer.
    /// </summary>
    /// <returns>Status och en lista över filmer.</returns>
    [HttpGet]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public async Task<ActionResult<ICollection<MovieDto>>> GetMovies()
    {
        var movies = await _movieService.GetMovies();
        return Ok(movies);
    }

    // GET: api/movies/5
    /// <summary>
    /// Hämtar en specifik film baserat på dess ID.
    /// </summary>
    /// <param name="id">ID för filmen som ska hämtas.</param>
    /// <returns>Status och den specifika filmen.</returns>
    [HttpGet("{id}")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public async Task<ActionResult<MovieDto>> GetMovieById(int id)
    {
        var movie = await _movieService.GetMovieById(id);

        if (movie == null)
        {
            return NotFound();
        }
        return Ok(movie);
    }

    // GET: api/movies/5/reviews
    /// <summary>
    /// Hämtar recensioner för en specifik film baserat på dess ID.
    /// </summary>
    /// <param name="id">ID för filmen som ska hämtas recensioner för.</param>
    /// <returns>Status och en lista över recensioner för filmen.</returns>
    [HttpGet("{id}/reviews")]
    [MapToApiVersion("2.0")]
    public async Task<ActionResult<MovieDto>> GetMovieReviews(int id)
    {
        var movie = await _movieService.GetMovieReviews(id);
        if (movie == null)
        {
            return NotFound();
        }
        var reviewDtos = movie.Reviews?.Select(r => new ReviewDto { ReviewId = r.ReviewId, Title = movie.Title, ReviewerName = r.ReviewerName, Rating = r.Rating, Comment = r.Comment }).ToList();
        return Ok(reviewDtos);
    }

    // GET: api/movies/5/details
    /// <summary>
    /// Hämtar detaljer för en specifik film baserat på dess ID.
    /// </summary>
    /// <param name="id">ID för filmen som ska hämtas detaljer för.</param>
    /// <returns>Status och detaljer för filmen.</returns>  
    [HttpGet("{id}/details")]
    [MapToApiVersion("2.0")]
    public async Task<ActionResult<MovieDto>> GetMovieDetails(int id)
    {
        var movie = await _movieService.GetMovieDetails(id);

        if (movie == null)
        {
            return NotFound();
        }
        var details = movie?.Details != null ? new MovieDetailsDto {
            Title = movie.Title,
            Synopsis = movie.Details.Synopsis,
            Language = movie.Details.Language,
            Budget = movie.Details.Budget
        } : null;

        return Ok(details);
    }

    // PUT: api/movie/2
    /// <summary>
    /// Uppdaterar en befintlig film baserat på dess ID.
    /// </summary>
    /// <param name="id">ID för filmen som ska uppdateras.</param>
    /// <param name="movieDto">Filmen med uppdaterad information.</param>
    /// <returns>Status för uppdateringsoperationen.</returns>  
    [HttpPut("{id}")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public async Task<IActionResult> PutMovie(int id, MovieUpdateDto movieDto)
    {
        return await _movieService.PutMovie(id, movieDto);
    }

    //POST: api/movie
    /// <summary>
    /// Skapar en ny film.
    /// </summary>
    /// <param name="movieDto">Filmen som ska skapas.</param>
    /// <returns>Status och den skapade filmen.</returns>
    [HttpPost]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public async Task<ActionResult<MovieDto>> PostMovie([FromBody] MovieCreateDto movieDto)
    {
        // Skapa en ny Movie baserat på DTO:n och spara de grundläggande fälten.
        return await _movieService.PostMovie(movieDto);
    }

    // DELETE: api/movie/5
    /// <summary>
    /// Tar bort en film baserat på dess ID.
    /// </summary>
    /// <param name="id">ID för filmen som ska tas bort.</param>
    /// <returns>Status för borttagningsoperationen.</returns>
    [HttpDelete("{id}")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        return await _movieService.DeleteMovie(id);
    }

    // POST: api/movie/5/actors/2
    /// <summary>
    /// Lägger till en skådespelare till en film baserat på deras ID.
    /// </summary>
    /// <param name="movieId">ID för filmen.</param>
    /// <param name="actorId">ID för skådespelaren.</param>
    /// <returns>Status för operationen.</returns>
    [HttpPost("{movieId}/actors/{actorId}")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> AddActorToMovie(int movieId, int actorId)
    {
        throw new NotImplementedException();
        //return await _movieService.AddActorToMovie(movieId, actorId);
    }
}
