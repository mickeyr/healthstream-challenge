using HealthStream.Data.Entities;

namespace HealthStream.Services.Authentication
{
    public interface IAuthenticationService
    {
        void RegisterUser(string username, string password, string email);
        bool IsUsernameAvailable(string username);
        User GetUserByUsername(string username);
        AuthenticationResponse AuthenticateUser(string username, string password);
        string ResetPasswordRequest(string emailAddress, string ipAddress);
        string ResetPassword(string token, string ipAddress, string newPassword);

    }
}
