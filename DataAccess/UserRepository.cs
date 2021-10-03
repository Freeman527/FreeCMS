using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using FreeCMS.Shared.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FreeCMS.DataAccess
{
    public class UserRepository : IUserRepository
    {

        private readonly IConfiguration _config;
        private readonly string connectionstring;

        public UserRepository (IConfiguration config) 
        {
            _config = config;
            connectionstring = @$"Server={_config["Database:DatabaseHost"]};
                                  Database={_config["Database:DatabaseName"]};
                                  User Id={_config["Database:DatabaseUser"]};
                                  Password={_config["Database:DatabasePassword"]};";
        }

        public List<UserUnit> GetUsers(string Username)
        {
            throw new System.NotImplementedException();
        }

        public bool RegisterUser(int UserId ,string Username, string Password, int UserClaimId)
        {
            SqlConnection dbconnection = new(connectionstring);

            dbconnection.Execute($"INSERT INTO users VALUES({UserId}, '{Username}', '{Password}', {UserClaimId})");
            return true;
        }

        public bool RemoveUser(int UserId)
        {
            SqlConnection dbconnection = new(connectionstring);

            dbconnection.Execute($"DELETE FROM users WHERE UserId = {UserId}");
            return true;
        }

        public bool UpdateUser(int UserId, string Username, string Password)
        {
            SqlConnection dbconnection = new(connectionstring);

            dbconnection.Execute($"UPDATE users SET Username = '{Username}', UserPassword = '{Password}' WHERE UserId = {UserId}");
            return true;
        }
    }
}