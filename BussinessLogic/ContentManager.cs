using FreeCMS.Entities;
using FreeCMS.DataAccess;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;


namespace FreeCMS.Managers
{
    public class ContentManager : IContentRepository
    {
        private readonly IConfiguration _config;

        // Dependency Injection 
        public ContentManager(IConfiguration config)
        {
            _config = config;
        }

        public bool AddContent(ContentUnitDTO input)
        {
            string connectionstring = $"Server={_config["Database:DatabaseHost"]};Database={_config["Database:DatabaseName"]};User Id={_config["Database:DatabaseUser"]};Password={_config["Database:DatabasePassword"]};";
            SqlConnection dbconnection = new(connectionstring);

            string ContentBodyJson = JsonConvert.SerializeObject(input.ContentBody);
            dbconnection.Execute($"INSERT INTO contents VALUES({input.ContentId},'{input.ContentName}','{ContentBodyJson}', {input.ContentOwnerId})");
            
            return true;
        }

        public List<ContentUnitDTO> GetContent(string ContentName)
        {
            string connectionstring = $"Server={_config["Database:DatabaseHost"]};Database={_config["Database:DatabaseName"]};User Id={_config["Database:DatabaseUser"]};Password={_config["Database:DatabasePassword"]};";
            SqlConnection dbconnection = new(connectionstring);

            if(ContentName == null ) 
            {
                List<ContentUnit> items = dbconnection.Query<ContentUnit>($"SELECT \"ContentId\", \"ContentName\", \"ContentBody\", \"ContentOwnerId\" FROM contents").ToList();
                List<ContentUnitDTO> itemsDTO = new();

                for (int i = 0; i < items.Count; i++)
                {
                    itemsDTO.Add(new ContentUnitDTO 
                    {
                        ContentId=items[i].ContentId, 
                        ContentName = items[i].ContentName,
                        ContentOwnerId = items[i].ContentOwnerId,
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
                    ContentOwnerId = items.First().ContentOwnerId,
                    ContentBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(items.First().ContentBody)
                });

                return itemsDTO;
            }
        }

        public bool UpdateContent (ContentUnitDTO input)
        {
            string connectionstring = $"Server={_config["Database:DatabaseHost"]};Database={_config["Database:DatabaseName"]};User Id={_config["Database:DatabaseUser"]};Password={_config["Database:DatabasePassword"]};";
            SqlConnection dbconnection = new(connectionstring);

            string ContentBodyJson = JsonConvert.SerializeObject(input.ContentBody);
            dbconnection.Execute($"UPDATE contents SET \"ContentName\" = '{input.ContentName}', \"ContentBody\" = '{ContentBodyJson}' WHERE \"ContentId\" = {input.ContentId}");

            return true;
        }

        public bool RemoveContent (int ContentId) 
        {
            string connectionstring = $"Server={_config["Database:DatabaseHost"]};Database={_config["Database:DatabaseName"]};User Id={_config["Database:DatabaseUser"]};Password={_config["Database:DatabasePassword"]};";
            SqlConnection dbconnection = new(connectionstring);

            dbconnection.Execute($"DELETE FROM contents WHERE \"ContentId\" = {ContentId}");

            return true;
        }
    }
}