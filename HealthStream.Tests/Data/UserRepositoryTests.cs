using System.Collections.Generic;
using System.Data;
using HealthStream.Data;
using HealthStream.Data.Entities;
using HealthStream.Data.Repositories;
using HealthStream.Tests.Data.Helpers;
using Moq;
using NUnit.Framework;

namespace HealthStream.Tests.Data
{
    [TestFixture]
    public class UserRepositoryTests
    {
        [Test]
        public void ShouldHaveMethodToGetUserById()
        {
            var expected = new User
            {
                Id = 5,
                Username = "Test"
            };
            var mockCommand = MockHelpers.CreateMockDbCommandForQuery<User>(new List<User> { expected });
            var mockUnitOfWOrk = new Mock<IUnitOfWork>();
            mockUnitOfWOrk.Setup(uow => uow.CreateCommand()).Returns(mockCommand.Object);
            var userRepository = new UserRepository(mockUnitOfWOrk.Object);
            var actual = userRepository.Get(5);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Username, actual.Username);
            Assert.AreEqual(1, mockCommand.Object.Parameters.Count);
        }
    }
}
