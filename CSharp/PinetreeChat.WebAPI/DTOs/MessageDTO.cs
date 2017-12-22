using PinetreeChat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinetreeChat.WebAPI.DTOs
{
    public class MessageDTO
    {
        public string ChatName { get; set; }
        public int Order { get; set; }
        public string From { get; set; }
        public string Text { get; set; }

        public MessageDTO()
        {

        }

        public MessageDTO(string chatName, Message entity)
        {
            ChatName = chatName;
            Order = entity.Order;
            From = entity.From.Username;
            Text = entity.Text;
        }

    }
}
