using PinetreeChat.Interfaces.Repositories;
using System.Collections.Generic;
using PinetreeChat.Entities;
using System.Linq;

namespace PinetreeChat.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static List<User> _users = new List<User>();

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public ICollection<User> GetUsers()
        {
            return _users.ToList();
        }

        public void SetLoggedIn(User user)
        {
            user.IsLoggedIn = true;
        }

        public void SetLoggedOut(User user)
        {
            user.IsLoggedIn = false;
        }
    }
}
