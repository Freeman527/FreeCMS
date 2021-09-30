using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace FreeCMS.DataAccess
{
    public class Repository
    {
        private readonly IConfiguration _config;

        public Repository(IConfiguration config) 
        {
            _config = config;
        }

        public void Execute(string sqlcommand) 
        {
            
        }

    }
}