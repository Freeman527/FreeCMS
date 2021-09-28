using FreeCMS.Entities;
using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System;

namespace FreeCMS.Managers
{
    public class ContentManager
    {
        private const string DbServerName = "freecms_db";
        private const string DbServerAdress = "127.0.0.1";
        private const string DbServerPort = "5432";
        private const string DbUserId = "postgres";
        private const string DbPassword = "adamadam41";

        public static string AddContent(ContentUnitDTO input)
        {
            var dbconnection = new NpgsqlConnection($"Server={DbServerAdress};Port={DbServerPort};Database={DbServerName};User Id={DbUserId};Password={DbPassword}");
            
            string ContentBodyJson = JsonConvert.SerializeObject(input.ContentBody);
            dbconnection.Execute($"INSERT INTO contents VALUES({input.ContentId},'{input.ContentName}','{ContentBodyJson}')");
            
            return "success";
        }

        public static string ReadContent(string ContentName) 
        {
            var dbconnection = new NpgsqlConnection($"Server={DbServerAdress};Port={DbServerPort};Database={DbServerName};User Id={DbUserId};Password={DbPassword}");
            
            List<ContentUnit> items = dbconnection.Query<ContentUnit>($"SELECT \"ContentId\", \"ContentName\", \"ContentBody\" FROM \"contents\" WHERE \"ContentName\" = '{ContentName}'").ToList();
            
            //converting ContentUnit to ConvertUnitDTO
            ContentUnitDTO itemDTO = new();
            itemDTO.ContentId = items.First().ContentId;
            itemDTO.ContentName = items.First().ContentName;
            itemDTO.ContentBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(items.First().ContentBody);

            return JsonConvert.SerializeObject(itemDTO);
        }
    }
}