using System.Collections.Generic;
using FreeCMS.Shared.Entities;

namespace FreeCMS.BussinessLogic
{
    public interface IUserService
    {
         string Authentication(string Username, string Password);
         List<UserUnit> GetUsers();
         bool RegisterUser(string Username, string Password, int UserRoleId);
         bool RemoveUser(int UserId);
         bool UpdateUser(int UserId, string Username, string Password);
    }
}