﻿using System;
using System.Collections.Generic;
using System.Linq;
using HealthStream.Data.Entities;
using HealthStream.Data.Repositories;

namespace HealthStream.Tests.TestRepositories
{
    class TestUserRepository : IUserRepository
    {
        private readonly List<User> _users;

        public TestUserRepository()
        {
            var rand = new Random();
            var baseUser = new User()
            {
                Username = "Malcolm",
                EmailAddress = "maroberts@gmail.com",
                CreatedOn = DateTime.UtcNow,
                Id = 1,
                FailedLoginAttempts = 0,
                PasswordHash = new byte[32],
                PasswordSalt = new byte[32]
            };
            rand.NextBytes(baseUser.PasswordSalt);
            rand.NextBytes(baseUser.PasswordHash);

            _users = new List<User>
            {
                baseUser
            };

        }

        public User Get(int id)
        {
            return _users.Single(u => u.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public void Delete(int id)
        {
            var user = Get(id);
            _users.Remove(user);
        }

        public void Udate(User entity)
        {
            var user = Get(entity.Id);
            user.Username = entity.Username;
            user.EmailAddress = entity.EmailAddress;
            user.PasswordSalt = entity.PasswordSalt;
            user.PasswordHash = entity.PasswordHash;
            user.ModifiedOn = DateTime.UtcNow;
        }

        public void Insert(User entity)
        {
            _users.Add(entity);
        }

        public void SaveChanges()
        {
            // Not needed
        }

        public User GetByUsername(string username)
        {
            return _users.SingleOrDefault(u => u.Username == username);
        }
    }
}
