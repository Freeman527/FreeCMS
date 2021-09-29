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
        private readonly IConfiguration _config;

        // Dependency Injection 
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

        public List<ContentUnitDTO> ReadContent(string ContentName)
        {
            var dbconnection = new NpgsqlConnection($"Server={_config["Database:DatabaseHost"]};Port={_config["Database:DatabasePort"]};Database={_config["Database:DatabaseName"]};User Id={_config["Database:DatabaseUser"]};Password={_config["Database:DatabasePassword"]}");

            if(ContentName == null ) 
            {
                List<ContentUnit> items = dbconnection.Query<ContentUnit>($"SELECT \"ContentId\", \"ContentName\", \"ContentBody\" FROM \"contents\"").ToList();
                List<ContentUnitDTO> itemsDTO = new();

                for (int i = 0; i < items.Count; i++)
                {
                    itemsDTO.Add(new ContentUnitDTO 
                    {
                        ContentId=items[i].ContentId, 
                        ContentName = items[i].ContentName, 
                        ContentBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(items[i].ContentBody)
                    });
                }

                return itemsDTO;
            } 
            else 
            {
                List<ContentUnit> items = dbconnection.Query<ContentUnit>($"SELECT \"ContentId\", \"ContentName\", \"ContentBody\" FROM \"contents\" WHERE \"ContentName\" = '{ContentName}'").ToList();

                List<ContentUnitDTO> itemsDTO = new();
                itemsDTO.Add(new ContentUnitDTO 
                {
                    ContentId=items.First().ContentId, 
                    ContentName = items.First().ContentName, 
                    ContentBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(items.First().ContentBody)
                });

                return itemsDTO;
            }
        }

        public string UpdateContent (ContentUnitDTO input)
        {
            var dbconnection = new NpgsqlConnection($"Server={_config["Database:DatabaseHost"]};Port={_config["Database:DatabasePort"]};Database={_config["Database:DatabaseName"]};User Id={_config["Database:DatabaseUser"]};Password={_config["Database:DatabasePassword"]}");

            string ContentBodyJson = JsonConvert.SerializeObject(input.ContentBody);
            dbconnection.Execute($"UPDATE contents SET \"ContentName\" = '{input.ContentName}', \"ContentBody\" = '{ContentBodyJson}' WHERE \"ContentId\" = {input.ContentId}");

            return $"item updated (id: '{input.ContentId}')";
        }

        public string DeleteContent (int ContentId) 
        {
            var dbconnection = new NpgsqlConnection($"Server={_config["Database:DatabaseHost"]};Port={_config["Database:DatabasePort"]};Database={_config["Database:DatabaseName"]};User Id={_config["Database:DatabaseUser"]};Password={_config["Database:DatabasePassword"]}");

            dbconnection.Execute($"DELETE FROM contents WHERE \"ContentId\" = {ContentId}");

            return $"item deleted (id: {ContentId})";
        }
    }
}
