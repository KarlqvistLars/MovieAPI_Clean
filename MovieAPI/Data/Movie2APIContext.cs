using Microsoft.EntityFrameworkCore;
using Movie_.Contracts;
using Movie_.Core.Models;

namespace Movie_.Data
{
    public class Movie2APIContext : DbContext, IMovie2APIContext
    {
        public Movie2APIContext(DbContextOptions<Movie2APIContext> options) : base(options)
        {
        }

        public DbSet<MovieClass> Movies { get; set; } = default!;
        public DbSet<MovieDetails> MovieDetails { get; set; } = default!;
        public DbSet<Genre> Genres { get; set; } = default!;
        public DbSet<Actor> Actors { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;
    }
}