using System;
using System.Runtime.Serialization;

namespace PinetreeChat.Domain.Services.Exceptions
{
    [Serializable]
    public class ChatExistsException : Exception
    {
        public ChatExistsException()
        {
        }

        public ChatExistsException(string chatName) : base($"Chat {chatName} already exists")
        {
        }

        public ChatExistsException(string chatName, Exception innerException) : base($"Chat {chatName} already exists", innerException)
        {
        }

        protected ChatExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}