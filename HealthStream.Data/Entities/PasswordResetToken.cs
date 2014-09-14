using System;

namespace HealthStream.Data.Entities
{
    public class PasswordResetToken
    {
        int Id { get; set; }
        int UserId { get; set; }
        string Token { get; set; }
        string IPAddress { get; set; }
        DateTime CreatedOn { get; set; }

    }
}
