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
    public class AccountController : Controller
    {
        AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }        

        [HttpPost("login")]
        public IActionResult Login([FromBody]AccountDTO loginDto)
        {
            try
            {
                _accountService.LogIn(loginDto.Username);
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
        
        [HttpPost("logout")]
        public IActionResult Logout([FromBody]AccountDTO loginDto)
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
