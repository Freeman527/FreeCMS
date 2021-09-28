using FreeCMS.Entities;
using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System;
using Microsoft.Extensions.Configuration;

namespace FreeCMS.Managers
{
    public class ContentManager
    {
        private IConfiguration _config;

        public ContentManager(IConfiguration config)
        {
            _config = config;
        }

        public string AddContent(ContentUnitDTO input)
        {
            var dbconnection = new NpgsqlConnection($"Server={_config["Database:DatabaseHost"]};Port={_config["Database:DatabasePort"]};Database={_config["Database:DatabaseName"]};User Id={_config["Database:DatabaseUser"]};Password={_config["Database:DatabasePassword"]}");

            string ContentBodyJson = JsonConvert.SerializeObject(input.ContentBody);
            dbconnection.Execute($"INSERT INTO contents VALUES({input.ContentId},'{input.ContentName}','{ContentBodyJson}')");

            return "success";
        }

        public ContentUnitDTO ReadContent(string ContentName)
        {
            var dbconnection = new NpgsqlConnection($"Server={_config["Database:DatabaseHost"]};Port={_config["Database:DatabasePort"]};Database={_config["Database:DatabaseName"]};User Id={_config["Database:DatabaseUser"]};Password={_config["Database:DatabasePassword"]}");

            List<ContentUnit> items = dbconnection.Query<ContentUnit>($"SELECT \"ContentId\", \"ContentName\", \"ContentBody\" FROM \"contents\" WHERE \"ContentName\" = '{ContentName}'").ToList();

            return new ContentUnitDTO()
            {
                ContentId = items.First().ContentId,
                ContentName = items.First().ContentName,
                ContentBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(items.First().ContentBody)
            };
        }
    }
}
