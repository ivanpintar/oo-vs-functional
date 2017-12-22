using System;
using Microsoft.AspNetCore.Mvc;
using PinetreeChat.Domain.Services;
using PinetreeChat.Domain.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using PinetreeChat.DataAccess.Repositories;
using PinetreeChat.WebAPI.DTOs;

namespace PinetreeChat.WebAPI.Controllers
{
    public class LoginController : Controller
    {
        LoginService _loginService;

        public LoginController()
        {
            _loginService = CreateLoginService();
        }        

        [HttpPost]
        [Route("api/login")]
        public IActionResult Login([FromBody]LoginDTO loginDto)
        {
            try
            {
                _loginService.LogIn(loginDto.Username);
            }
            catch (UserExistsException)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }
        
        [HttpPost]
        [Route("api/logout")]
        public IActionResult Logout([FromBody]LoginDTO loginDto)
        {
            try
            {
                _loginService.LogOut(loginDto.Username);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        private LoginService CreateLoginService()
        {
            return new LoginService(new UserRepository());
        }
    }
}
