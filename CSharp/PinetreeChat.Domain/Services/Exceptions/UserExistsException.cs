using System;
using System.Runtime.Serialization;

namespace PinetreeChat.Domain.Services.Exceptions
{
    [Serializable]
    public class UserExistsException : Exception
    {
        public UserExistsException()
        {
        }

        public UserExistsException(string username) : base($"User {username} already exists")
        {
        }

        public UserExistsException(string username, Exception innerException) : base($"User {username} already exists", innerException)
        {
        }

        protected UserExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}