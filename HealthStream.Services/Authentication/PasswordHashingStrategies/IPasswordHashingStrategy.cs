namespace HealthStream.Services.Authentication.PasswordHashingStrategies
{
    internal interface IPasswordHashingStrategy
    {
        bool ValidatePasswordHash(string password, string hash);
        string HashPassword(string password);
       
    }
}
