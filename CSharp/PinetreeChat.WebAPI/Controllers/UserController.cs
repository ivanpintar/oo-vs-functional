using System;
using Microsoft.AspNetCore.Mvc;
using PinetreeChat.Domain.Services;
using PinetreeChat.Domain.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using PinetreeChat.DataAccess.Repositories;
using PinetreeChat.WebAPI.DTOs;

namespace PinetreeChat.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        UserService _accountService;

        public UserController(UserService accountService)
        {
            _accountService = accountService;
        }        

        [HttpPost("login")]
        public IActionResult Login([FromBody]UserDTO loginDto)
        {
            try
            {
                _accountService.LogIn(loginDto.Username);
                return Ok();
            }
            catch (UsernameInvalidException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UserExistsException)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpPost("logout")]
        public IActionResult Logout([FromBody]UserDTO loginDto)
        {
            try
            {
                _accountService.LogOut(loginDto.Username);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }
    }
}
