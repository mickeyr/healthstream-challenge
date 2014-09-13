using System;

namespace HealthStream.Services.Authentication.Exceptions
{
    [Serializable]
    public class UsernameExistsException : Exception
    {
        public UsernameExistsException(string message)
            : base(message)
        {
        }
    }
}
