using System.Collections.Generic;
using System.Data.SqlClient;
using FreeCMS.Managers;
using FreeCMS.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FreeCMS.Presentation.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        
        private readonly UserManager _userManager;

        public UserController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("/api/users/authentication")]
        public IActionResult Authentication(string username, string password) 
        {
            var token = _userManager.Authentication(username, password);
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

        [HttpPost("/api/users/register")]
        public bool RegisterUser(int UserId, string Username, string Password, int UserClaimId) 
        {
            return _userManager.RegisterUser(UserId, Username, Password, UserClaimId);
        }

        [HttpPut("/api/users")]
        public bool UpdateUser(int UserId, string Username, string Password) 
        {
            return _userManager.UpdateUser(UserId, Username, Password);
        }

        [HttpDelete("/api/users")]
        public bool RemoveUser(int UserId) 
        {
            return _userManager.RemoveUser(UserId);
        }
    }
}