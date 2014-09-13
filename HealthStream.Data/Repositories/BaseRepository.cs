using System.Data;

namespace HealthStream.Data.Repositories
{
    public abstract class BaseRepository
    {
        protected IUnitOfWork _unitOfWork;

        protected BaseRepository(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
    }
}