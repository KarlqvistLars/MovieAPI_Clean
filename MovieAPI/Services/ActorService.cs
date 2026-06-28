using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.Core;
using Movie_.Contracts;
using Movie_.Core;
using Movie_.Core.DomainContracts;
using Movie_.Core.ModelDto;
using Movie_.Core.Models;

namespace Movie_.API.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _actorRepository;
        public ActorService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }
        public async Task<ICollection<ActorDto>> GetActors()
        {
            var actors = await _actorRepository.GetAllAsync();
            return actors.Select(a => new ActorDto {
                ActorId = a.ActorId,
                Name = a.Name,
                BirthYear = a.BirthYear,
                Movies = a.Movies!
                    .Select(m => m.Title)
                    .ToList()
            }).ToList();
        }
        public async Task<ActorDto?> GetActor(int actorid)
        {
            var actor = await _actorRepository.GetAsync(actorid);

            if (actor == null) { return null; }

            return new ActorDto {
                ActorId = actor.ActorId,
                Name = actor.Name,
                BirthYear = actor.BirthYear,
                Movies = actor.Movies!
                    .Select(m => m.Title)
                    .ToList()
            };
        }
        public async Task<IActionResult> PutActor(int actorid, ActorUpdateDto actor)
        {
            if (actorid != actor.ActorId) return new BadRequestResult();
            var existingActor = await _actorRepository.GetAsync(actorid);

            if (existingActor == null)
            {
                return new NotFoundResult();
            } else
            {
                existingActor.Name = actor.Name;
                existingActor.BirthYear = actor.BirthYear;
                existingActor.Movies = new List<MovieClass>(); // Movies are not updated through this endpoint
                try
                {
                    _actorRepository.Update(existingActor);
                } catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actorid))
                    {
                        return new NotFoundResult();
                    } else
                    {
                        throw;
                    }
                }
            }
            return new OkResult();
        }
        public async Task<ActionResult<ActorDto>> PostActor(ActorCreateDto actorDto)
        {
            if (actorDto == null)
            {
                return new NotFoundResult();
            }
            var actor = new Actor {
                Name = actorDto.Name,
                BirthYear = actorDto.BirthYear,
            };
            _actorRepository.Add(actor);
            return new ActorDto {
                ActorId = actor.ActorId,
                Name = actor.Name,
                BirthYear = actor.BirthYear
            };
        }
        public async Task<IActionResult> DeleteActor(int actorid)
        {
            var actor = await _actorRepository.GetAsync(actorid);
            if (actor == null)
            {
                return new NotFoundResult();
            }
            _actorRepository.Remove(actor);
            return new NoContentResult();
        }
        private bool ActorExists(int actorid)
        {
            return _actorRepository.AnyAsync(actorid).Result;
        }
    }
}

