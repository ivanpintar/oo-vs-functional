using PinetreeChat.Domain.Services.Exceptions;
using PinetreeChat.Entities;
using PinetreeChat.Interfaces.Repositories;
using System.Linq;

namespace PinetreeChat.Domain.Services
{
    public class AccountService
    {
        private IUserRepository _userRepo;

        public AccountService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }        

        public void LogIn(string username)
        {
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
