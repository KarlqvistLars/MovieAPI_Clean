using Microsoft.AspNetCore.Mvc;
using Movie_.Contracts;
using Movie_.Core.DomainContracts;
using Movie_.Core.ModelDto;
using Movie_.Core.Models;

namespace Movie_.Core.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieRepository _movieRepository;
        public ReviewService(IReviewRepository reviewRepository, IMovieRepository movieRepository)
        {
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
        }

        public async Task<ICollection<ReviewDto>> GetReviews()
        {
            var review = await _reviewRepository.GetAllAsync();
            return review.Select(r => new ReviewDto {
                ReviewId = r.ReviewId,
                Title = r.Movie.Title,
                ReviewerName = r.ReviewerName,
                Comment = r.Comment,
                Rating = r.Rating,
            }).ToList();
        }

        public async Task<ReviewDto?> GetReview(int id)
        {
            var review = await _reviewRepository.GetAsync(id);
            if (review == null) { return null; }
            return new ReviewDto {
                ReviewId = review.ReviewId,
                Title = review.Movie.Title,
                ReviewerName = review.ReviewerName,
                Comment = review.Comment,
                Rating = review.Rating
            };
        }

        public async Task<bool> PutReview(int id, ReviewUpdateDto reviewDto)
        {
            var review = await _reviewRepository.GetAsync(id);
            if (review == null)
            {
                return false;
            }
            review.ReviewerName = reviewDto?.ReviewerName ?? review.ReviewerName;
            review.Comment = reviewDto?.Comment ?? review.Comment;
            review.Rating = reviewDto?.Rating ?? review.Rating;
            _reviewRepository.Update(review);
            return true;
        }

        public async Task<ReviewDto?> PostReview(int movieId, ReviewDto reviewDto)
        {
            var movie = await _movieRepository.GetAsync(movieId);

            if (movie == null)
            {
                return null;
            }

            var review = new Review {
                MovieId = movieId,
                ReviewerName = reviewDto?.ReviewerName ?? string.Empty,
                Comment = reviewDto?.Comment ?? string.Empty,
                Rating = reviewDto?.Rating ?? 0
            };
            // spara review i databasen
            _reviewRepository.Add(review);
            // lägg till review i movie.Reviews
            movie.Reviews.Add(review);
            _movieRepository.Update(movie);

            return new ReviewDto {
                ReviewId = review.ReviewId,
                Title = movie.Title,
                ReviewerName = review.ReviewerName,
                Comment = review.Comment,
                Rating = review.Rating
            };
        }

        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _reviewRepository.GetAsync(id);
            if (review == null)
            {
                return new NotFoundResult();
            }

            _reviewRepository.Remove(review);
            return new NoContentResult();
        }
    }
}
