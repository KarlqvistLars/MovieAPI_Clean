using Microsoft.AspNetCore.Mvc;
using Movie.Core;
using Movie_.Core;
using Movie_.Core.ModelDto;

namespace Movie_.Contracts
{
    public interface IActorService
    {
        public Task<ICollection<ActorDto>> GetActors();
        public Task<ActorDto?> GetActor(int actorid);
        public Task<IActionResult> PutActor(int actorid, ActorUpdateDto actor);
        public Task<ActionResult<ActorDto>> PostActor(ActorCreateDto actorDto);
        public Task<IActionResult> DeleteActor(int actorid);
    }
}