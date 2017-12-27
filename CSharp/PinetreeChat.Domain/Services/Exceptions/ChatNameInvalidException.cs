using System;
using System.Runtime.Serialization;

namespace PinetreeChat.Domain.Services.Exceptions
{
    [Serializable]
    public class ChatNameInvalidException : Exception
    {
        public ChatNameInvalidException()
        {
        }

        public ChatNameInvalidException(string chatName) : base($"Chat name {chatName} is invalid")
        {
        }

        public ChatNameInvalidException(string chatName, Exception innerException) : base($"Chat name {chatName} is invalid", innerException)
        {
        }

        protected ChatNameInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}