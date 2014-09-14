using HealthStream.Data.Entities;

namespace HealthStream.Data.Repositories
{
    public interface IUserTokenRepository
    {
        PasswordResetToken GenerateTokenForUser(User user);
    }
}
