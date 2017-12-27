using System;
using System.Runtime.Serialization;

namespace PinetreeChat.Domain.Services.Exceptions
{
    [Serializable]
    public class UsernameInvalidException : Exception
    {
        public UsernameInvalidException()
        {
        }

        public UsernameInvalidException(string username) : base($"Username {username} is invalid")
        {
        }

        public UsernameInvalidException(string username, Exception innerException) : base($"Username {username} is invalid", innerException)
        {
        }

        protected UsernameInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}