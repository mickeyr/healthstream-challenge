using System;
using HealthStream.Data.Entities;
using HealthStream.Data.Repositories;
using HealthStream.Services.Authentication.Exceptions;
using HealthStream.Services.Authentication.PasswordHashingStrategies;

namespace HealthStream.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingStrategy _hashingStrategy;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _hashingStrategy = new PBKDF2PasswordHashingStrategy();
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
                throw new UsernameExistsException(String.Format("The username {0} already exists.", username));
            }

            var user = new User
            {
                Username = username,
                EmailAddress = email,
                PasswordHash = _hashingStrategy.HashPassword(password)
            };

            _userRepository.Insert(user);
            _userRepository.SaveChanges();
        }

        public bool IsUsernameAvailable(string username)
        {
            return GetUserByUsername(username) == null;
        }

        public User GetUserByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }

        public AuthenticationResponse AuthenticateUser(string username, string password)
        {
            var response = new AuthenticationResponse();
            var user = GetUserByUsername(username);
            if (user == null)
            {
                response.Result = AuthenticationResult.InvalidUsernameOrPassword;
                return response;
            }

            
            if (_hashingStrategy.ValidatePasswordHash(password, user.PasswordHash))
            {
                response.Result = AuthenticationResult.Success;
                response.Token = "token";
            }
            else
            {
                response.Result = AuthenticationResult.InvalidUsernameOrPassword;
            }

            return response;
        }

        public string ResetPasswordRequest(string emailAddress, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public string ResetPassword(string token, string ipAddress, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}