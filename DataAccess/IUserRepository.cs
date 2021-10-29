using System.Collections.Generic;
using FreeCMS.Shared.Entities;

namespace FreeCMS.DataAccess
{
    public interface IUserRepository
    {
         List<UserUnit> GetUsers();
         bool RegisterUser(int UserId,string Username, string Password, int UserClaimId);
         bool UpdateUser(int UserId, string Username, string Password);
         bool RemoveUser(int UserId);
    }
}