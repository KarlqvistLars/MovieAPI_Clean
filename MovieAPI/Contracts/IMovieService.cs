using Microsoft.AspNetCore.Mvc;
using Movie_.Core;
using Movie_.Core.ModelDto;

namespace Movie_.Contracts
{
    public interface IMovieService
    {
        public Task<ICollection<MovieDto>> GetMovies();
        public Task<MovieDto?> GetMovieById(int id);
        public Task<MovieDto?> GetMovieReviews(int id);
        public Task<MovieDto?> GetMovieDetails(int id);
        public Task<IActionResult> PutMovie(int id, MovieUpdateDto movieDto);
        Task<ActionResult<MovieDto>> PostMovie(MovieCreateDto movieDto);
        public Task<IActionResult> DeleteMovie(int id);
        Task<IActionResult> AddActorToMovie(int movieId, int actorId);
    }
}
