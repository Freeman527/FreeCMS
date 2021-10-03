using System.Collections.Generic;
using FreeCMS.Managers;
using FreeCMS.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FreeCMS.Presentation.Controllers
{
    public class UserController : Controller
    {
        
        private readonly UserManager _userManager;

        public UserController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("/api/users")]
        public List<UserUnit> GetUsers() 
        {
            return null;
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