using Microsoft.EntityFrameworkCore;
using Movie_.Core.DomainContracts;
using Movie_.Core.Models;
using Movie_.Data;

namespace Movie_.Core.Data.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly Movie2APIContext _db;
        public ActorRepository(Movie2APIContext db)
        {
            _db = db;
        }
        async Task<IEnumerable<Actor>> IActorRepository.GetAllAsync()
        {
            var actors = await _db.Actors
                .Include(a => a.Movies)
                .Select(a => new Actor {
                    ActorId = a.ActorId,
                    Name = a.Name,
                    BirthYear = a.BirthYear,
                    Movies = a.Movies
                        .Select(m => new MovieClass {
                            MovieClassId = m.MovieClassId,
                            Title = m.Title,
                            Year = m.Year,
                            Duration = m.Duration
                        })
                        .ToList()
                })
                .ToListAsync();
            return actors;
        }
        async Task<Actor?> IActorRepository.GetAsync(int id)
        {
            var actor = await _db.Actors
                .Include(a => a.Movies)
                .FirstOrDefaultAsync(a => a.ActorId == id) ?? null;

            if (actor == null) { return null; }

            return new Actor {
                ActorId = actor.ActorId,
                Name = actor.Name,
                BirthYear = actor.BirthYear,
                Movies = actor.Movies!
                    .Select(m => new MovieClass {
                        MovieClassId = m.MovieClassId,
                        Title = m.Title,
                        Year = m.Year,
                        Duration = m.Duration
                    })
                    .ToList()
            };
        }
        Task<bool> IActorRepository.AnyAsync(int id)
        {
            throw new NotImplementedException();
        }
        void IActorRepository.Add(Actor actor)
        {
            if (actor == null)
            {
                return;
            }
            _db.Actors.Add(actor);
        }
        void IActorRepository.Update(Actor actor)
        {
            if (actor == null)
            {
                return;
            }
            _db.Actors.Update(actor);
        }
        void IActorRepository.Remove(Actor actor)
        {
            if (actor == null)
            {
                return;
            }
            _db.Actors.Remove(actor);
        }
    }
}
