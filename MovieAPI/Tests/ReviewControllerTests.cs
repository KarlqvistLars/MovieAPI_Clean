using Microsoft.AspNetCore.Mvc;
using Moq;
using Movie_.Contracts;
using Movie_.Core.ModelDto;

namespace Movie_.Tests
{
    public class ReviewControllerTests
    {
        [Fact]
        public async Task GetReviews_ReturnsOkWithReviews()
        {

            // Arrange
            ICollection<ReviewDto> reviews = new List<ReviewDto>()
            {
                new ReviewDto {
                ReviewId = 1,
                Rating = 5,
                Comment = "Great movie!" },
                new ReviewDto {
                ReviewId = 2,
                Rating = 4,
                Comment = "Good movie!" }
            };

            var mockService = new Mock<IReviewService>();

            mockService
                .Setup(s => s.GetReviews())
                .ReturnsAsync(reviews);

            var controller = new ReviewsController(mockService.Object);

            // Act
            var result = await controller.GetReviews();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var returnedReviews = Assert.IsType<List<ReviewDto>>(okResult.Value);

            Assert.Equal(2, returnedReviews.Count);
            Assert.Equal(1, returnedReviews[0].ReviewId);
            Assert.Equal("Great movie!", returnedReviews[0].Comment);
            Assert.Equal(2, returnedReviews[1].ReviewId);
            Assert.Equal("Good movie!", returnedReviews[1].Comment);
        }
        [Fact]
        public async Task GetReviewById_ReturnsOkWithReviews()
        {
            // Arrange
            var reviewDto = new ReviewDto {
                ReviewId = 1,
                Rating = 5,
                Comment = "Great movie!"
            };

            var mockService = new Mock<IReviewService>();

            mockService.Setup(r => r.GetReview(1))
                .ReturnsAsync(reviewDto);

            var controller = new ReviewsController(mockService.Object);

            // Act
            var result = await controller.GetReview(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var returnedReview = Assert.IsType<ReviewDto>(okResult.Value);

            Assert.Equal(1, returnedReview.ReviewId);
            Assert.Equal(5, returnedReview.Rating);
            Assert.Equal("Great movie!", returnedReview.Comment);
        }
        [Fact]
        public async Task GetReviewDetails_ReturnsOK()
        {
            // Arrange
            var movieDto = new MovieDto {
                MovieId = 1,
                Title = "Test Movie",
                Year = null,
                Duration = null,
                Details = new MovieDetailsDto {
                    Synopsis = "SynopsisTest.",
                    Language = "Engelska",
                    Budget = "$Test"
                },
                Actors = new List<ActorDto>(),
                Genres = new List<GenreDto>(),
                Reviews = new List<ReviewDto>()
            };

            var mockService = new Mock<IMovieService>();

            mockService
                .Setup(s => s.GetMovieDetails(1))
                .ReturnsAsync(movieDto);

            var controller = new MoviesController(mockService.Object);

            // Act
            var result = await controller.GetMovieDetails(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedMovie = Assert.IsType<MovieDetailsDto>(okResult.Value);
            Assert.Equal("Test Movie", returnedMovie.Title);

            var returnedDetails = Assert.IsType<MovieDetailsDto>(okResult.Value);
            Assert.Equal("SynopsisTest.", returnedDetails.Synopsis);
            Assert.Equal("Engelska", returnedDetails.Language);
            Assert.Equal("$Test", returnedDetails.Budget);
        }
        [Fact]
        public async Task GetMovieReviewByMovieId_ReturnsOKWithReviews()
        {
            // Arrange
            var movieDto = new MovieDto {
                MovieId = 1,
                Title = "Test Movie",
                Year = null,
                Duration = null,
                Details = new MovieDetailsDto(),
                Actors = new List<ActorDto>(),
                Genres = new List<GenreDto>(),
                Reviews = new List<ReviewDto> {
                    new ReviewDto {
                        ReviewId = 1,
                        ReviewerName = "Test Reviewer",
                        Rating = 5,
                        Comment = "Great movie!"
                    }
                }
            };

            var mockService = new Mock<IMovieService>();

            mockService
                .Setup(s => s.GetMovieReviews(1))
                .ReturnsAsync(movieDto);

            var controller = new MoviesController(mockService.Object);

            // Act
            var result = await controller.GetMovieReviews(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var reviews = Assert.IsType<List<ReviewDto>>(okResult.Value);

            var returnedReviews = Assert.IsType<List<ReviewDto>>(reviews);
            Assert.Single(returnedReviews);
            //Assert.Equal(1, returnedReviews[0].Id);
            Assert.Equal("Test Reviewer", returnedReviews[0].ReviewerName);
            Assert.Equal(5, returnedReviews[0].Rating);
            Assert.Equal("Great movie!", returnedReviews[0].Comment);
        }
    }
}

