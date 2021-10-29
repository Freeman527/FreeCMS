using System.Collections.Generic;
using FreeCMS.DataAccess;
using FreeCMS.Shared.Entities;

namespace FreeCMS.Managers
{
    public class UserManager
    {
        private readonly UserRepository _userRepository;

        public UserManager(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string Authentication(string username, string password) 
        {
            return _userRepository.Authentication(username, password);
        }

        public List<UserUnit> GetUsers() 
        {
            return _userRepository.GetUsers();
        }

        public bool RegisterUser(int UserId, string Username, string Password, int UserClaimId) 
        {
            return _userRepository.RegisterUser(UserId, Username, Password, UserClaimId);
        }

        public bool RemoveUser(int UserId) 
        {
            return _userRepository.RemoveUser(UserId);
        }

        public bool UpdateUser(int UserId, string Username, string Password) 
        {
            return _userRepository.UpdateUser(UserId, Username, Password);
        }
    }
}