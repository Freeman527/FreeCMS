using System.Collections.Generic;
using FreeCMS.DataAccess;
using FreeCMS.Shared.Entities;

namespace FreeCMS.Managers
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string Authentication(string Username, string Password) 
        {
            return _userRepository.Authentication(Username, Password);
        }

        public List<UserUnit> GetUsers() 
        {
            return _userRepository.GetUsers();
        }

        public bool RegisterUser(string Username, string Password, int UserRoleId) 
        {
            return _userRepository.RegisterUser(Username, Password, UserRoleId);
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