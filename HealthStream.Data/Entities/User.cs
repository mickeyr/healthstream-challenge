﻿using System;

namespace HealthStream.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public int FailedLoginAttempts { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
