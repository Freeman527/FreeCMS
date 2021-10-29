using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using FreeCMS.Shared.Entities;
using FreeCMS.Shared.Security;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FreeCMS.DataAccess
{
    public class UserRepository : IUserRepository
    {

        private readonly IConfiguration _config;
        private readonly JwtAuthenticateManager _jwtAuthManager;
        private readonly string connectionstring;

        public UserRepository (IConfiguration config, JwtAuthenticateManager jwtAuthManager) 
        {
            _config = config;
            _jwtAuthManager = jwtAuthManager;
            connectionstring = @$"Server={_config["Database:DatabaseHost"]};
                                  Database={_config["Database:DatabaseName"]};
                                  User Id={_config["Database:DatabaseUser"]};
                                  Password={_config["Database:DatabasePassword"]};";
        }

        public List<UserUnit> GetUsers()
        {
            SqlConnection dbconnection = new(connectionstring);

            List<UserUnit> users = dbconnection.Query<UserUnit>("SELECT UserId, Username, UserPassword as password FROM users").ToList();

            return users;
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

        public string Authentication(string username, string password) 
        {
            SqlConnection dbconnection = new(connectionstring);

            List<UserUnit> usercreds = dbconnection.Query<UserUnit>($"SELECT Username, UserPassword as Password FROM users WHERE Username = '{username}' and UserPassword = '{password}'").ToList();

            if (usercreds.Any()) 
            {
                return _jwtAuthManager.Authenticate(usercreds.First().Username, usercreds.First().Password);
            } 
            else 
            {
                return null;
            }
        }
    }
}