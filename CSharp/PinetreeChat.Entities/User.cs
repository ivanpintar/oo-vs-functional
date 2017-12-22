namespace PinetreeChat.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public bool IsLoggedIn { get; set; } 

        public User(string username)
        {
            Username = username;
            IsLoggedIn = false;
        }
    }
}
