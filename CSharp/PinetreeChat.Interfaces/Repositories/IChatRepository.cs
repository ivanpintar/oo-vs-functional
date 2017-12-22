using PinetreeChat.Entities;
using System.Collections.Generic;

namespace PinetreeChat.Interfaces.Repositories
{
    public interface IChatRepository
    {
        ICollection<Chat> GetChats();
        void AddChat(Chat chat);
        void AddMessage(Chat chat, Message message);
        void AddParticipant(Chat chat, User user);
        void RemoveParticipant(Chat chat, User user);
    }
}
