using System;
using HealthStream.Data.Entities;
using HealthStream.Data.Repositories;

namespace HealthStream.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void RegisterUser(string username, string password, string email)
        {
            if (String.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException("username", "Username is required.");
            }

            if (String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("password", "Password is required.");
            }

            if (String.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException("email", "Email address is required.");
            }

            if (!IsUsernameAvailable(username))
            {
                throw new ArgumentException(String.Format("The username {0} already exists.", username), "username");
            }

            var user = new User()
            {
                Username = username,
                EmailAddress = email
            };

            _userRepository.Insert(user);
        }

        public bool IsUsernameAvailable(string username)
        {
            return _userRepository.GetByUsername(username) == null;
        }

        public User GetUserByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }
    }
}