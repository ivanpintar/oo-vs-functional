using PinetreeChat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinetreeChat.WebAPI.DTOs
{
    public class ChatDTO
    {
        public string Name { get; set; }
        public List<string> Participants { get; set; }
        public List<MessageDTO> Messages { get; set; }

        public ChatDTO()
        {
            Participants = new List<string>();
            Messages = new List<MessageDTO>();
        }

        public ChatDTO(Chat entity)
        {
            Name = entity.Name;
            Participants = entity.Participants.Select(p => p.Username).ToList();
            Messages = entity.Messages.Select(m => new MessageDTO(entity.Name, m)).ToList();
        }
    }
}
