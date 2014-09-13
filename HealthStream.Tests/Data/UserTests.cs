//using System;
//using System.Configuration;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;
//using HealthStream.Data;
//using HealthStream.Data.Entities;
//using HealthStream.Data.Repositories;
//using NUnit.Framework;

//namespace HealthStream.Tests.Data
//{
//    [TestFixture]
//    public class UserTests
//    {
//        [Test]
//        public void SpikeAddingUserToDatabase()
//        {
//            var unitOfWorkFactory = new AdoNetUnitOfWorkFactory();
//            var rand = new Random();

            
//            var expected = new User
//            {
//                Username = "Malcolm",
//                EmailAddress = "maroberts@gmail.com",
//                PasswordSalt = new Byte[32],
//                PasswordHash = new Byte[32]
//            };

//            rand.NextBytes(expected.PasswordHash);
//            rand.NextBytes(expected.PasswordSalt);

//            using (var uow = unitOfWorkFactory.Create(ConfigurationManager.ConnectionStrings["HealthStreamDb"]))
//            {
//                var repository = new UserRepository(uow);
//                repository.Insert(expected);
//                var actual = repository.Get(expected.Id);
//                Assert.AreEqual(expected.Id, actual.Id);
//                Assert.AreEqual(expected.Username, actual.Username);
//                Assert.AreEqual(expected.EmailAddress, actual.EmailAddress);
//                Assert.AreEqual(expected.PasswordSalt, actual.PasswordSalt);
//                Assert.AreEqual(expected.PasswordHash, actual.PasswordHash);
//                Assert.IsTrue(actual.CreatedOn > DateTime.MinValue);
//                Assert.AreEqual(expected.ModifiedOn, actual.ModifiedOn);
//                Assert.AreEqual(expected.FailedLoginAttempts, actual.FailedLoginAttempts);

//                var users = repository.GetAll();
//                Assert.AreEqual(1, users.Count());

//                rand.NextBytes(expected.PasswordHash);
//                repository.Udate(expected);
//                actual = repository.Get(expected.Id);
//                Assert.AreEqual(expected.Id, actual.Id);
//                Assert.AreEqual(expected.Username, actual.Username);
//                Assert.AreEqual(expected.EmailAddress, actual.EmailAddress);
//                Assert.AreEqual(expected.PasswordSalt, actual.PasswordSalt);
//                Assert.AreEqual(expected.PasswordHash, actual.PasswordHash);
//                Assert.IsTrue(actual.CreatedOn > DateTime.MinValue);
//                Assert.AreNotEqual(expected.ModifiedOn, actual.ModifiedOn);
//                Assert.AreEqual(expected.FailedLoginAttempts, actual.FailedLoginAttempts);

//                repository.Delete(expected.Id);

//                var user = repository.GetByUsername("NOT THERE");
//                Assert.IsNull(user);
//            }

//        }
//    }
//}
