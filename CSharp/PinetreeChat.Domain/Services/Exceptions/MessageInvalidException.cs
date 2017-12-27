using System;
using System.Runtime.Serialization;

namespace PinetreeChat.Domain.Services.Exceptions
{
    [Serializable]
    public class MessageInvalidException : Exception
    {
        public MessageInvalidException()
        {
        }

        public MessageInvalidException(string message) : base($"Message {message} is invalid")
        {
        }

        public MessageInvalidException(string message, Exception innerException) : base($"Message {message} is invalid", innerException)
        {
        }

        protected MessageInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}