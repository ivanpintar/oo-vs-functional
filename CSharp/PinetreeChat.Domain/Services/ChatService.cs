using PinetreeChat.Domain.Services.Exceptions;
using PinetreeChat.Entities;
using PinetreeChat.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PinetreeChat.Domain.Services
{
    public class ChatService
    {
        private IChatRepository _chatRepo;
        private IUserRepository _userRepo;

        public ChatService(IChatRepository chatRepo, IUserRepository userRepo)
        {
            _chatRepo = chatRepo;
            _userRepo = userRepo;
        }

        public IEnumerable<Chat> GetChats()
        {
            return _chatRepo.GetChats();
        }

        public Chat GetChat(string chatName)
        {
            return _chatRepo.GetChats().SingleOrDefault(c => c.Name == chatName);
        }

        public Chat CreateChat(string chatName)
        {
            if(chatName.Length > 50 || string.IsNullOrWhiteSpace(chatName))
            {
                throw new ChatNameInvalidException(chatName);
            }

            var chats = _chatRepo.GetChats();
            if (chats.Any(c => c.Name.ToLower() == chatName.ToLower()))
            {
                throw new ChatExistsException(chatName);
            }

            var newChat = new Chat(chatName);
            _chatRepo.AddChat(newChat);
            return newChat;
        }

        public Message AddMessage(string chatName, string text, string from)
        {
            if(text.Length > 150 || string.IsNullOrWhiteSpace(text))
            {
                throw new MessageInvalidException(text);
            }

            var chat = _chatRepo.GetChats().SingleOrDefault(c => c.Name == chatName);
            var user = _userRepo.GetUsers().SingleOrDefault(c => c.Username == from);
            if (chat == null)
            {
                return null;
            }

            var message = new Message(user, text);
            _chatRepo.AddMessage(chat, message);

            if (!chat.Participants.Any(p => p.Username == from))
            {
                _chatRepo.AddParticipant(chat, user);
            }

            return message;
        }

        public void LeaveChat(string chatName, string participant)
        {
            var chat = _chatRepo.GetChats().SingleOrDefault(c => c.Name == chatName);
            var user = _userRepo.GetUsers().SingleOrDefault(c => c.Username == participant);
            _chatRepo.RemoveParticipant(chat, user);
        }
    }
}
