using FreeCMS.DataAccess;

namespace FreeCMS.Managers
{
    public class UserManager
    {
        private readonly UserRepository _userRepository;

        public UserManager(UserRepository userRepository)
        {
            _userRepository = userRepository;
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