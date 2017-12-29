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
                Response.StatusCode = 400;
                return Json(ex.Message);
            }
            catch (UserExistsException ex)
            {
                Response.StatusCode = 409;
                return Json(ex.Message);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message);
            }
        }
        
        [HttpPost("logout")]
        public IActionResult Logout([FromBody]UserDTO loginDto)
        {
            try
            {
                _accountService.LogOut(loginDto.Username);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message);
            }

            return Ok();
        }
    }
}
