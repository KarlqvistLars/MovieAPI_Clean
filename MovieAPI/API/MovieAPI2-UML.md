```mermaid
classDiagram

	class Program {
		+getMovies()
		+getMovieById(id)
		}

	class Actor {
		+ActorId: int
		+Name: string
		+BirthYear: string
		}

	class Movie {
		+MovieId: int
		+Title: string
		+Year: string
        +Duration: string
        + DetailsId: int
        + Details : MovieDetails
        +  Actors :ICollection<Actor>
        + Genres :ICollection<Genre>
        + MovieActors :ICollection<MovieActor>
        + Reviews :ICollection<Review>
		}

	class MovieActor {
		+MovieId: int
        +Movie : Movie
		+ActorId: int
        +Actor : Actor
        +Id: int
		}

	class MovieDetails {
        +MovieDetailsId : int
        +Synopsis : string
        +Language : string
        +Budget : string
		+Movie : Movie
		}

	class Review {
        +ReviewId : int
        +ReviewerName : string
        +Comment : string
        +Rating : int
        +MovieId : int
        +Movie : Movie
		}



	class ActorDto {
		+ActorId: int
		+Name: string
		+BirthYear: string
		}

	class MovieDto {
		+MovieId: int
		+Title: string
		+Year: string
        +Duration: string
        + DetailsId: int
        + Details : MovieDetails
        +  Actors :ICollection<Actor>
        + Genres :ICollection<Genre>
        + MovieActors :ICollection<MovieActor>
        + Reviews :ICollection<Review>
		}

	class MovieActorDto {
		+MovieId: int
        +Movie : Movie
		+ActorId: int
        +Actor : Actor
        +Id: int
		}

	class MovieDetailsDto {
        +MovieDetailsId : int
        +Synopsis : string
        +Language : string
        +Budget : string
		+Movie : Movie
		}

	class ReviewDto {
        +ReviewId : int
        +ReviewerName : string
        +Comment : string
        +Rating : int
        +MovieId : int
        +Movie : Movie
		}


```