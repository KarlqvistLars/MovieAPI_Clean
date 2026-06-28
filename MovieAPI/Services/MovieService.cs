using Microsoft.AspNetCore.Mvc;
using Movie_.Contracts;
using Movie_.Core.DomainContracts;
using Movie_.Core.ModelDto;
using Movie_.Core.Models;

namespace Movie_.Core.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;
        private readonly IReviewRepository _reviewRepository;

        public MovieService(IMovieRepository movieRepository, IActorRepository actorRepository, IReviewRepository reviewRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
            _reviewRepository = reviewRepository;
        }
        public async Task<ICollection<MovieDto>> GetMovies()
        {
            // Använd Repository metoderna genom IMovieRepository.
            var movies = await _movieRepository.GetAllAsync();

            // Mappa entiteter till DTO:er
            var movieDtos = movies.Select(m => new MovieDto {
                MovieId = m.MovieClassId,
                Title = m.Title,
                Year = m.Year,
                Duration = m.Duration,
                Details = m.Details != null ? new MovieDetailsDto {
                    Synopsis = m.Details.Synopsis,
                    Language = m.Details.Language,
                    Budget = m.Details.Budget
                } : null,
                Actors = m.Actors?.Select(a => new ActorDto {
                    ActorId = a.ActorId,
                    Name = a.Name,
                    BirthYear = a.BirthYear,
                    Movies = a.Movies?.Select(am => am.Title).ToList() ?? new List<string>()
                }).ToList(),
                Genres = m.Genres?.Select(g => new GenreDto { GenreId = g.GenreId, GenreName = g.GenreName }).ToList(),
                Reviews = m.Reviews?.Select(r => new ReviewDto { ReviewId = r.ReviewId, ReviewerName = r.ReviewerName, Rating = r.Rating, Comment = r.Comment }).ToList()
            }).ToList();
            return movieDtos;
        }
        public async Task<MovieDto?> GetMovieById(int id)
        {
            var movie = await _movieRepository.GetAsync(id) ?? null;
            if (movie == null)
            {
                return null;
            }
            var movieDto = new MovieDto {
                MovieId = movie?.MovieClassId ?? 0,
                Title = movie?.Title ?? string.Empty,
                Year = movie?.Year ?? string.Empty,
                Duration = movie?.Duration ?? string.Empty,
                Details = movie?.Details is { } details
                ? new MovieDetailsDto {
                    Synopsis = details.Synopsis ?? string.Empty,
                    Language = details.Language ?? string.Empty,
                    Budget = details.Budget ?? string.Empty
                } : null,
                Actors = movie?.Actors?.Select(a => new ActorDto {
                    ActorId = a.ActorId,
                    Name = a.Name,
                    BirthYear = a.BirthYear,
                    Movies = a.Movies?.Select(am => am.Title).ToList() ?? new List<string>()
                }).ToList(),
                Genres = movie?.Genres?.Select(g => new GenreDto { GenreId = g.GenreId, GenreName = g.GenreName }).ToList(),
                Reviews = movie?.Reviews?.Select(r => new ReviewDto { ReviewId = r.ReviewId, ReviewerName = r.ReviewerName, Rating = r.Rating, Comment = r.Comment }).ToList()
            };
            return movieDto;
        }
        public async Task<MovieDto?> GetMovieReviews(int id)
        {
            var movie = await _movieRepository.GetAsync(id) ?? null;
            if (movie == null)
            {
                return null;
            }
            var reviewDtos = new MovieDto {
                MovieId = movie?.MovieClassId ?? 0,
                Title = movie?.Title ?? string.Empty,
                Reviews = movie?.Reviews?.Select(r => new ReviewDto { ReviewerName = r.ReviewerName ?? string.Empty, Rating = r.Rating, Comment = r.Comment ?? string.Empty }).ToList()
            };
            return reviewDtos;
        }
        public async Task<MovieDto?> GetMovieDetails(int id)
        {
            var movie = await _movieRepository.GetAsync(id);

            if (movie == null)
            {
                return null;
            }
            var movieDetails = new MovieDto {
                MovieId = movie.MovieClassId,
                Title = movie.Title,
                Details = movie.Details is { } details
                    ? new MovieDetailsDto {
                        Synopsis = details.Synopsis ?? string.Empty,
                        Language = details.Language ?? string.Empty,
                        Budget = details.Budget ?? string.Empty
                    }
                    : null
            };
            return movieDetails;
        }
        public async Task<IActionResult> PutMovie(int id, MovieUpdateDto movieDto)
        {
            // Hämta befintlig film inklusive dess relaterade data
            var existingMovie = await _movieRepository.GetAsync(id);

            if (existingMovie == null) { return new NotFoundResult(); }
            if (movieDto == null) { return new BadRequestResult(); }

            // Uppdatera enkla fält
            existingMovie.Title = movieDto.Title;
            existingMovie.Year = movieDto.Year;
            existingMovie.Duration = movieDto.Duration;
            // Uppdatera detaljer
            if (movieDto.Details != null)
            {
                if (existingMovie.Details != null && movieDto?.Details != null)
                {
                    existingMovie.Details.Synopsis = movieDto.Details.Synopsis;
                    existingMovie.Details.Language = movieDto.Details.Language;
                    existingMovie.Details.Budget = movieDto.Details.Budget;
                }
            }
            // TODO: Hantera listor (Actors/Genres/Reviews) på ett korrekt sätt, t.ex. genom att jämföra befintliga och nya listor och uppdatera dem istället för att ta bort och lägga till på nytt.
            //Hantera listor(Actors/ Genres)
            if (existingMovie.Genres != null) { _movieRepository.Remove(existingMovie); }
            existingMovie.Genres = movieDto!.Genres?.Select(g => new Genre { GenreName = g.GenreName })
                .ToList() ?? new List<Genre>();

            //if (existingMovie.Actors != null) { _actorRepository.Remove(existingMovie.get(existingMovie.Actors.Id)); }
            //existingMovie.Actors = movieDto!.Actors?.Select(a => new Actor { Name = a.Name })
            //    .ToList() ?? new List<Actor>();

            //if (existingMovie.Reviews != null) { _reviewRepository.Remove(existingMovie.Reviews); }
            //existingMovie.Reviews = movieDto!.Reviews?.Select(r => new Review { ReviewerName = r.ReviewerName, Rating = r.Rating, Comment = r.Comment })
            //    .ToList() ?? new List<Review>();
            _movieRepository.Update(existingMovie);
            return new NoContentResult();
        }
        public async Task<ActionResult<MovieDto>> PostMovie(MovieCreateDto movieDto)
        {
            // Skapa en ny Movie-entitet baserat på DTO:n och spara de grundläggande fälten, lägg sedan till relaterade data (Details, Actors, Genres, Reviews)
            var movie = new MovieClass {
                Title = movieDto.Title ?? string.Empty,
                Year = movieDto.Year,
                Duration = movieDto.Duration,
                Genres = new List<Genre>(),
                Actors = new List<Actor>()
            };


            // Spara MovieDetails
            if (movieDto.Details != null)
            {
                movie.Details = new MovieDetails {
                    Synopsis = movieDto.Details.Synopsis,
                    Language = movieDto.Details.Language,
                    Budget = movieDto.Details.Budget
                };
            }
            // Spara Actors
            var existingActors = await _actorRepository.GetAllAsync();

            if (movieDto.Actors != null)
            {
                foreach (var actorDto in movieDto.Actors)
                {
                    var actor = await _movieRepository.GetActorByNameAndBirthYearAsync(
                        actorDto.Name ?? string.Empty,
                        actorDto.BirthYear ?? string.Empty);

                    movie.Actors.Add(actor ?? new Actor {
                        Name = actorDto.Name ?? string.Empty,
                        BirthYear = actorDto.BirthYear ?? string.Empty
                    });
                }
            }

            var existingMovies = await _movieRepository.GetAllAsync();

            var existingGenres = existingMovies
                .SelectMany(m => m.Genres)
                .DistinctBy(g => g.GenreName)
        .ToList();

            if (movieDto.Genres != null)
            {
                foreach (var genreDto in movieDto.Genres)
                {
                    var existingGenre = existingGenres
                        .FirstOrDefault(g => g.GenreName == genreDto.GenreName);

                    if (existingGenre != null)
                    {
                        movie.Genres.Add(existingGenre);
                    } else
                    {
                        movie.Genres.Add(new Genre {
                            GenreName = genreDto.GenreName ?? string.Empty
                        });
                    }
                }
            }
            // Spara Review
            if (movieDto.Reviews != null)
            {
                foreach (var reviewDto in movieDto.Reviews)
                {
                    movie.Reviews.Add(new Review { ReviewerName = reviewDto.ReviewerName ?? string.Empty, Rating = reviewDto.Rating, Comment = reviewDto.Comment ?? string.Empty });
                }
            }

            // Lägg till den nya filmen i databasen
            _movieRepository.Add(movie);

            // Returnera resultatet med CreatedAtAction som pekar på GetMovie filmdatat.
            var result = new MovieDto {
                MovieId = movie.MovieClassId,
                Title = movie.Title,
                Year = movie.Year,
                Duration = movie.Duration,

                Details = movie.Details == null ? null : new MovieDetailsDto {
                    Synopsis = movie.Details.Synopsis,
                    Language = movie.Details.Language,
                    Budget = movie.Details.Budget
                },

                Actors = movie.Actors.Select(a => new ActorDto {
                    ActorId = a.ActorId,
                    Name = a.Name,
                    BirthYear = a.BirthYear
                }).ToList(),

                Genres = movie.Genres.Select(g => new GenreDto {
                    GenreId = g.GenreId,
                    GenreName = g.GenreName
                }).ToList(),

                Reviews = movie.Reviews.Select(r => new ReviewDto {
                    ReviewId = r.ReviewId,
                    ReviewerName = r.ReviewerName,
                    Rating = r.Rating,
                    Comment = r.Comment
                }).ToList()
            };

            return new CreatedAtActionResult(
                nameof(GetMovieById),
                "Movies",
                new { id = movie.MovieClassId },
               result);
        }
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _movieRepository.GetAsync(id);
            if (movie == null)
            {
                return new NotFoundResult();
            }
            _movieRepository.Remove(movie);
            return new NoContentResult();
        }

        //TODO: Implementera metoden AddActorToMovie för att lägga till en skådespelare i en film.
        public async Task<IActionResult> AddActorToMovie(int movieId, int actorId)
        {
            throw new NotImplementedException();
        }
        private bool MovieExists(int id)
        {
            return _movieRepository.AnyAsync(id).Result;
        }
    }
}

