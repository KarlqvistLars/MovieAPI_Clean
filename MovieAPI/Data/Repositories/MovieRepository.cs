using Microsoft.EntityFrameworkCore;
using Movie_.Core.DomainContracts;
using Movie_.Core.Models;
using Movie_.Data;

namespace Movie_.Core.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly Movie2APIContext _db;
        public MovieRepository(Movie2APIContext db)
        {
            _db = db;
        }
        async Task<Actor?> IMovieRepository.GetActorByNameAndBirthYearAsync(string name, string birthYear)
        {
            return await _db.Actors
                .FirstOrDefaultAsync(a => a.Name == name && a.BirthYear == birthYear);
        }
        async Task<IEnumerable<MovieClass>> IMovieRepository.GetAllAsync()
        {
            // Vi använder .Include för att hämta den relaterade datan
            var movies = await _db.Movies
                .Include(m => m.Details)   // Hämtar MovieDetails
                .Include(m => m.Actors)    // Hämtar listan av Actors
                .Include(m => m.Genres)    // Hämtar listan av Genres
                .Include(m => m.Reviews)   // Hämtar listan av Reviews
                .ToListAsync();
            return movies;
        }
        async Task<MovieClass?> IMovieRepository.GetAsync(int id)
        {
            var movie = await _db.Movies
                .Include(m => m.Details)   // Hämtar MovieDetails
                .Include(m => m.Actors)    // Hämtar listan av Actors
                .Include(m => m.Genres)    // Hämtar listan av Genres
                .Include(m => m.Reviews)   // Hämtar listan av Reviews
                .FirstOrDefaultAsync(m => m.MovieClassId == id) ?? null;
            if (movie == null)
            {
                return null;
            }
            return movie;
        }
        Task<bool> IMovieRepository.AnyAsync(int id)
        {
            // Kontrollera om en film med det angivna id:t finns i databasen
            return _db.Movies.AnyAsync(e => e.MovieClassId == id);
        }
        void IMovieRepository.Add(MovieClass movie)
        {
            // Lägg till den nya filmen i databasen
            _db.Movies.Add(movie);
            _db.SaveChanges();
        }
        void IMovieRepository.Update(MovieClass movie)
        {
            _db.Update(movie);
            _db.SaveChanges();
        }
        void IMovieRepository.Remove(MovieClass movie)
        {
            _db.Movies.Remove(movie);
            _db.SaveChanges();
        }
    }
}