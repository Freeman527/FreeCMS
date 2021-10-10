using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using FreeCMS.Shared.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FreeCMS.DataAccess
{
    public class ContentRepository : IContentRepository
    {
        private readonly IConfiguration _config;
        private readonly string connectionstring;

        public ContentRepository(IConfiguration config)
        {
            _config = config;
            connectionstring = @$"Server={_config["Database:DatabaseHost"]};
                                  Database={_config["Database:DatabaseName"]};
                                  User Id={_config["Database:DatabaseUser"]};
                                  Password={_config["Database:DatabasePassword"]};";
        }

        public bool AddContent(ContentUnitDTO input)
        {
            SqlConnection dbconnection = new(connectionstring);

            string inputJson = JsonConvert.SerializeObject(input.ContentBody);
            dbconnection.Execute(@$"INSERT INTO contents (ContentName, ContentBody, ContentOwnerId, Date)
                                    VALUES('{input.ContentName}', '{inputJson}', {input.ContentOwnerId}, {DateTimeOffset.Now.ToUnixTimeSeconds()})");
            return true;
        }

        public bool AddContent(string contentBody, string contentName, int ownerId)
        {
            throw new NotImplementedException();
        }

        // public bool AddContent(ContentUnitDTO input)
        // {
        //     SqlConnection dbconnection = new(connectionstring);

        //     string inputJson = JsonConvert.SerializeObject(input);
        //     dbconnection.Execute(@$"INSERT INTO contents SELECT * FROM OPENJSON('{inputJson}') WITH (
        //                             ContentName varchar(128) '$.ContentName',
        //                             ContentBody nvarchar(max) '$.ContentBody' AS JSON,
        //                             ContentOwnerId int '$.ContentOwnerId'
        //                             )");
        //     return true;
        // }

        public List<ContentUnitDTO_output> GetContent(ContentSearchUnit input)
        {
            SqlConnection dbconnection = new(connectionstring);

            //unix timestamp converter
            static DateTime UnixTimeStampToDateTime(uint unixTimeStamp)
            {
                // Unix timestamp is seconds past epoch
                DateTime dtDateTime = new(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
                return dtDateTime;
            }

            //only one content that searchable by name
            if (input.SearchAll == false)
            {
                List<ContentUnit> items = dbconnection.Query<ContentUnit>(@$"SELECT ContentId, ContentName, ContentBody, Username as ContentOwner, Date FROM contents 
                                                                            INNER JOIN users u on u.UserId = contents.ContentOwnerId 
                                                                            WHERE ContentName = '{input.ContentName}'").ToList();
                List<ContentUnitDTO_output> itemsDTO = new();

                if (items.Count > 0)
                {
                    itemsDTO.Add(new ContentUnitDTO_output
                    {
                        ContentId = items.First().ContentId,
                        ContentName = items.First().ContentName,
                        ContentOwner = items.First().ContentOwner,
                        Date = UnixTimeStampToDateTime(items.First().Date),
                        ContentBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(items.First().ContentBody)
                    });

                    return itemsDTO;
                }
                else
                {
                    return null;
                }
            }
            //SearchAll and other filters
            else
            {
                string limitString = $"FETCH NEXT {input.Limit} ROWS ONLY";

                if (input.Limit <= 0)
                {
                    limitString = null;
                }

                List<ContentUnit> items = dbconnection.Query<ContentUnit>(@$"SELECT ContentId, ContentName, ContentBody, Username as ContentOwner, Date FROM contents 
                                                                             INNER JOIN users u on u.UserId = contents.ContentOwnerId 
                                                                             ORDER BY ContentId OFFSET {input.Offset} ROWS {limitString}").ToList();
                List<ContentUnitDTO_output> itemsDTO = new();

                for (int i = 0; i < items.Count; i++)
                {
                    itemsDTO.Add(new ContentUnitDTO_output
                    {
                        ContentId = items[i].ContentId,
                        ContentName = items[i].ContentName,
                        ContentOwner = items[i].ContentOwner,
                        Date = UnixTimeStampToDateTime(items[i].Date),
                        ContentBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(items[i].ContentBody)
                    });
                }

                return itemsDTO;
            }
        }

        public bool RemoveContent(int ContentId)
        {
            SqlConnection dbconnection = new(connectionstring);

            dbconnection.Execute($"DELETE FROM contents WHERE \"ContentId\" = {ContentId}");

            return true;
        }

        public bool UpdateContent(int ContentId, ContentUnitDTO input)
        {
            SqlConnection dbconnection = new(connectionstring);

            string ContentBodyJson = JsonConvert.SerializeObject(input.ContentBody);
            dbconnection.Execute($"UPDATE contents SET \"ContentName\" = '{input.ContentName}', \"ContentBody\" = '{ContentBodyJson}' WHERE \"ContentId\" = {ContentId}");

            return true;
        }

        public bool UpdateContent(int contentId, string newContentBody)
        {
            throw new NotImplementedException();
        }
    }
}