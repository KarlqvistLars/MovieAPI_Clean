using Movie_.Core.Models;

namespace Movie_.Core.DomainContracts
{
    public interface IMovieRepository
    {
        Task<Actor?> GetActorByNameAndBirthYearAsync(string name, string birthYear);
        Task<IEnumerable<MovieClass>> GetAllAsync();
        Task<MovieClass?> GetAsync(int id);
        Task<bool> AnyAsync(int id);
        void Add(MovieClass movie);
        void Update(MovieClass movie);
        void Remove(MovieClass movie);
    }
}
