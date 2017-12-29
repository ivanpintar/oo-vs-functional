using PinetreeChat.Domain.Services.Exceptions;
using PinetreeChat.Entities;
using PinetreeChat.Interfaces.Repositories;
using System.Linq;

namespace PinetreeChat.Domain.Services
{
    public class UserService
    {
        private IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }        

        public void LogIn(string username)
        {
            if(username.Length > 20 || string.IsNullOrWhiteSpace(username))
            {
                throw new UsernameInvalidException(username);
            }

            var user = _userRepo.GetUsers().SingleOrDefault(u => u.Username.ToLower() == username.ToLower());
            if(user != null && user.IsLoggedIn)
            {
                throw new UserExistsException(username);
            }
            else if(user != null && !user.IsLoggedIn)
            {
                _userRepo.SetLoggedIn(user);
            }
            else
            {
                user = new User(username);
                _userRepo.AddUser(user);
                _userRepo.SetLoggedIn(user);
            }
        }

        public void LogOut(string username)
        {
            var user = _userRepo.GetUsers().SingleOrDefault(u => u.Username.ToLower() == username.ToLower());
            if (user == null)
            {
                return;
            }
            
            _userRepo.SetLoggedOut(user);
        }
    }
}
