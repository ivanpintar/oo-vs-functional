using PinetreeChat.Entities;
using System.Collections.Generic;

namespace PinetreeChat.Interfaces.Repositories
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        void AddUser(User user);
        void SetLoggedIn(User user);
        void SetLoggedOut(User user);
    }
}
