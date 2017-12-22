using System.Collections.Generic;
using System.Linq;

namespace PinetreeChat.Entities
{
    public class Chat
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public ICollection<User> Participants { get; private set; }
        public ICollection<Message> Messages { get; private set; }

        public Chat(string name)
        {
            Name = name;
            Participants = new List<User>();
            Messages = new List<Message>();
        }
    }
}
