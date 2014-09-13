using HealthStream.Data.Entities;

namespace HealthStream.Services
{
    public interface IAuthenticationService
    {
        void RegisterUser(string username, string password, string email);
        bool IsUsernameAvailable(string username);
        User GetUserByUsername(string username);
    }
}
