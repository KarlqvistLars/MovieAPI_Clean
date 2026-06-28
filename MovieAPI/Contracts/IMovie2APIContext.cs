using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Movie_.Core.Models;

namespace Movie_.Contracts
{
    public interface IMovie2APIContext
    {
        DbSet<MovieClass> Movies { get; set; }
        DbSet<MovieDetails> MovieDetails { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Actor> Actors { get; set; }
        DbSet<Review> Reviews { get; set; }
        EntityEntry Entry(object entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken =
        default);
    }
}
