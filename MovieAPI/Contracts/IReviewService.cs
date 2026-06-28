using Microsoft.AspNetCore.Mvc;
using Movie_.Core;
using Movie_.Core.ModelDto;

namespace Movie_.Contracts
{
    public interface IReviewService
    {
        public Task<ICollection<ReviewDto>> GetReviews();

        public Task<ReviewDto?> GetReview(int id);

        public Task<bool> PutReview(int id, ReviewUpdateDto review);

        public Task<ReviewDto?> PostReview(int movieId, ReviewDto reviewDto);

        public Task<IActionResult> DeleteReview(int id);

    }
}
