namespace PinetreeChat.Entities
{
    public class Message
    {
        public int Id { get; private set; }
        public int Order { get; set; }
        public User From { get; private set; }
        public string Text { get; private set; }

        public Message(User from, string text)
        {
            Order = 0;
            From = from;
            Text = text;
        }

        public Message(int order, User from, string text) : this(from, text)
        {
            Order = order;
        }
    }
}