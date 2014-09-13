using System;
using HealthStream.Services.Authentication;
using HealthStream.Tests.Services.TestRepositories;
using NUnit.Framework;

namespace HealthStream.Tests.Services
{
    [TestFixture]
    public class TestAuthenticationService
    {
        private IAuthenticationService _authenticationService;

        [SetUp]
        public void Setup()
        {
            _authenticationService = new AuthenticationService(new TestUserRepository()); 
        }

        [Test]
        public void IsUsernameAvailableShouldReturnTrueWhenUsernameIsAvailable()
        {
            _authenticationService.IsUsernameAvailable("New User");
        }

        [Test]
        public void IsUsernameAvailableShouldReturnFalseWhenUsernameIsNotAvailable()
        {
            _authenticationService.IsUsernameAvailable("Malcolm");
        }


        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CallingRegisterWithNullNameShouldThrowArgumentNullException()
        {
            _authenticationService.RegisterUser(null, "password", "email");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CallingRegisterWithNullPasswordShouldThrowArgumentNullException()
        {
            _authenticationService.RegisterUser("username", null, "email");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CallingRegisterWithNullEmailShouldThrowArgumentNullException()
        {
            _authenticationService.RegisterUser("username", "password", null);
        }

        [Test]
        [ExpectedException(typeof (ArgumentException))]
        public void CallingRegisterWithExistingUsernameShouldThrowArgumentException()
        {
            _authenticationService.RegisterUser("Malcolm", "test", "maroberts@gmail.com");
        }

        [Test]
        public void CallingRegisterWithValidDataShouldAddUser()
        {
            _authenticationService.RegisterUser("Mickey", "test", "mickey@mickeyr.com");
            var user = _authenticationService.GetUserByUsername("Mickey");
            Assert.IsNotNull(user);
        }

        [Test]
        public void UserAuthenticationReturnsAuthenticationResponseObject()
        {
            var results = _authenticationService.AuthenticateUser("Mickey", "test");
            Assert.IsInstanceOf<AuthenticationResponse>(results);
        }

        [Test]
        public void CanAuthenticateRegisteredUser()
        {
            _authenticationService.RegisterUser("Mickey", "test", "mickey@mickeyr.com");
            var user = _authenticationService.GetUserByUsername("Mickey");
            Assert.IsNotNull(user, "Unable to insert new user");

            var results = _authenticationService.AuthenticateUser("Mickey", "test");
            Assert.AreEqual(AuthenticationResult.Success, results.Result);
        }

        [Test]
        public void SuccessfulAuthenticationReturnsAToken()
        {
            _authenticationService.RegisterUser("Mickey", "test", "mickey@mickeyr.com");
            var user = _authenticationService.GetUserByUsername("Mickey");
            Assert.IsNotNull(user, "Unable to insert new user");

            var results = _authenticationService.AuthenticateUser("Mickey", "test");
            Assert.IsFalse(String.IsNullOrWhiteSpace(results.Token));
        }

        [Test]
        public void TryingToAuthenticateUnregisteredUserReturnsInvalidUsernameOrPasswordResponse()
        {
            var results = _authenticationService.AuthenticateUser("Mickey", "test");
            Assert.AreEqual(AuthenticationResult.InvalidUsernameOrPassword, results.Result);
        }
    }
}
