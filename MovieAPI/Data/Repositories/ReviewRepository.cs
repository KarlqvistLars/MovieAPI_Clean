using Microsoft.EntityFrameworkCore;
using Movie_.Core.DomainContracts;
using Movie_.Core.Models;
using Movie_.Data;

namespace Movie_.Core.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly Movie2APIContext _db;
        public ReviewRepository(Movie2APIContext db)
        {
            _db = db;
        }
        async Task<IEnumerable<Review>> IReviewRepository.GetAllAsync()
        {
            var review = await _db.Reviews
                .Include(m => m.Movie)
                .ToListAsync();
            return review.AsEnumerable();
        }
        async Task<Review?> IReviewRepository.GetAsync(int id)
        {
            var review = await _db.Reviews
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(r => r.ReviewId == id);
            if (review == null)
            {
                return null;
            }
            return review;
        }
        Task<bool> IReviewRepository.AnyAsync(int id)
        {
            // Kollar om en recension finns i databasen
            return _db.Reviews.AnyAsync(r => r.ReviewId == id);
        }
        void IReviewRepository.Add(Review review)
        {
            // Lägger till en ny recension i databasen
            _db.Reviews.Add(review);
            _db.SaveChanges();
        }
        void IReviewRepository.Update(Review review)
        {
            // Updaterar en befintlig recension i databasen
            _db.Reviews.Update(review);
            _db.SaveChanges();
        }
        void IReviewRepository.Remove(Review review)
        {
            // Tar bort en recension från databasen
            _db.Reviews.Remove(review);
            _db.SaveChanges();
        }
    }
}