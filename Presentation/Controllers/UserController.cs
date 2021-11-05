using System.Collections.Generic;
using System.Data.SqlClient;
using FreeCMS.Managers;
using FreeCMS.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FreeCMS.Presentation.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        
        private readonly UserService _userManager;

        public UserController(UserService userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("/api/users/authentication/{Username}")]
        public IActionResult Authentication(string Username, string Password) 
        {
            var token = _userManager.Authentication(Username, Password);
            if(token == null) 
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        [HttpGet("/api/users")]
        public List<UserUnit> GetUsers() 
        {
            return _userManager.GetUsers();
        }

        [AllowAnonymous]
        [HttpPost("/api/users/register/{Username}")]
        public bool RegisterUser(string Username, string Password) 
        {
            return _userManager.RegisterUser(Username, Password, 2);
        }

        [HttpPut("/api/users/{Username}")]
        public bool UpdateUser(int UserId, string Username, string Password) 
        {
            return _userManager.UpdateUser(UserId, Username, Password);
        }

        [HttpDelete("/api/users{UserId}")]
        public bool RemoveUser(int UserId) 
        {
            return _userManager.RemoveUser(UserId);
        }
    }
}