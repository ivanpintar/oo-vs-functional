using PinetreeChat.Interfaces.Repositories;
using System.Collections.Generic;
using PinetreeChat.Entities;
using System.Linq;

namespace PinetreeChat.DataAccess.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private static List<Chat> _chats = new List<Chat>();


        public ICollection<Chat> GetChats()
        {
            return _chats.ToList();
        }

        public void AddChat(Chat chat)
        {
            _chats.Add(chat);
        }

        public void AddMessage(Chat chat, Message message)
        {
            message.Order = chat.Messages.Count;
            chat.Messages.Add(message);
        }

        public void AddParticipant(Chat chat, User user)
        {
            chat.Participants.Add(user);
        }

        public void RemoveParticipant(Chat chat, User user)
        {
            chat.Participants.Remove(user);
        }
    }
}
