using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Movie_.Contracts;
using Movie_.Core;
using Movie_.Core.ModelDto;

// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;
    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    // GET: api/review
    /// <summary>
    /// Hämtar en lista över alla recensioner.
    /// </summary>
    /// <returns>Status och en lista över alla recensioner.</returns>
    [HttpGet]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public async Task<ActionResult<ICollection<ReviewDto>>> GetReviews()
    {
        var reviews = await _reviewService.GetReviews();
        if (!reviews.Any()) { return NotFound(); }
        return Ok(reviews);
    }

    // GET: api/reviews/5
    /// <summary>
    /// Hämtar en specifik recension baserat på dess ID.
    /// </summary>
    /// <param name="id">ID för recensionen som ska hämtas.</param>
    /// <returns>Status och den specifika recensionen.</returns>
    [HttpGet("{id}")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public async Task<ActionResult<ReviewDto>> GetReview(int id)
    {
        var review = await _reviewService.GetReview(id);
        if (review == null) { return NotFound(); }
        return Ok(review);
    }

    // PUT: api/review/5
    /// <summary>
    /// Uppdaterar en specifik recension baserat på dess ID.
    /// </summary>
    /// <param name="id">ID för recensionen som ska uppdateras.</param>
    /// <param name="review">Recensionen med uppdaterad information.</param>
    /// <returns>Status för uppdateringsoperationen.</returns>
    [HttpPut("{id}")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public async Task<IActionResult> PutReview(int id, ReviewUpdateDto review)
    {
        bool updated = await _reviewService.PutReview(id, review);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/review
    /// <summary>
    /// Skapar en ny recension för en specifik film.
    /// </summary>
    /// <param name="movieId">ID för filmen som recensionen ska skapas för.</param>
    /// <param name="reviewDto">Recensionen som ska skapas.</param>
    /// <returns>Status och den skapade recensionen.</returns>
    [HttpPost("{movieId}")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public async Task<ActionResult<ReviewDto>> PostReview(
        int movieId,
        [FromBody] ReviewDto reviewDto)
    {
        var result = await _reviewService.PostReview(movieId, reviewDto);
        if (result == null) { return NotFound(); }
        return CreatedAtAction(
            nameof(GetReview),
            new { id = result.ReviewId },
            result);
    }

    // DELETE: api/review/5
    /// <summary>
    /// Tar bort en specifik recension baserat på dess ID.
    /// </summary>
    /// <param name="id">ID för recensionen som ska tas bort.</param>
    /// <returns>Status för borttagningsoperationen.</returns>
    [HttpDelete("{id}")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var result = await _reviewService.DeleteReview(id);
        return result;
    }

}
