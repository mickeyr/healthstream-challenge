using HealthStream.Data.Entities;

namespace HealthStream.Data.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
        User GetByUsername(string username);
    }
}
