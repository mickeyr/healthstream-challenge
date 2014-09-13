namespace HealthStream.Data.Repositories
{
    public abstract class BaseRepository
    {
        protected IUnitOfWork UnitOfWork;

        protected BaseRepository(IUnitOfWork unitOfWork) {
            UnitOfWork = unitOfWork;
        }
    }
}