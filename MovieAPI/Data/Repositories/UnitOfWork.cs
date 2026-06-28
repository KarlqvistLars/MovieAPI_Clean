using Movie_.Core.DomainContracts;
using Movie_.Data;

namespace Movie_.Core.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Movie2APIContext _db;

        public UnitOfWork(Movie2APIContext db)
        {
            _db = db;
        }
        // TODO: Implement the repositories and CompleteAsync method in UnitOfWork class
        public IMovieRepository Movies => throw new NotImplementedException();

        public IReviewRepository Reviews => throw new NotImplementedException();

        public IActorRepository Actors => throw new NotImplementedException();

        public Task CompleteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
