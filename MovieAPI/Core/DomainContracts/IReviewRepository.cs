using Movie_.Core.Models;

namespace Movie_.Core.DomainContracts
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review?> GetAsync(int id);
        Task<bool> AnyAsync(int id);
        void Add(Review review);
        void Update(Review review);
        void Remove(Review review);
    }
}
