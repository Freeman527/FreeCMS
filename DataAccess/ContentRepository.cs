using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using Dapper;
using FreeCMS.Presentation.Formatters;
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

        public bool AddContent(string contentType, string contentBody, ClaimsPrincipal user)
        {
            SqlConnection dbconnection = new(connectionstring);

            try
            {
                 var jsonString = JsonConvert.DeserializeObject(contentBody);
            }
            catch
            {
                return false;                
            }

            if(contentType == null) 
            {
                return false;
            }

            dbconnection.Execute(@$"INSERT INTO contents (ContentType, ContentBody, Date)
                                    VALUES('{contentType}', '{contentBody}', {DateTimeOffset.Now.ToUnixTimeSeconds()})");

            return true;
        }

        public string GetContent(int contentId) 
        {
            SqlConnection dbconnection = new(connectionstring);

            List<ContentUnit> queryContent = dbconnection.Query<ContentUnit>($"SELECT * FROM contents WHERE ContentId = {contentId}").ToList();
            List<ContentUnitDTO_output> contentDTO = new();

            contentDTO.Add(new ContentUnitDTO_output {
                ContentId = queryContent.First().ContentId,
                ContentType = queryContent.First().ContentType,
                ContentBody = JsonConvert.DeserializeObject<Dictionary<string, object>>(queryContent.First().ContentBody),
                Date = UnixTimeStampToDateTimeConverter.UnixTimeStampToDateTime(queryContent.First().Date)
            });

            return JsonConvert.SerializeObject(contentDTO.First());
        }

        public List<ContentUnitDTO_output> GetContents(string contentType, int offset, int pageSize, string orderField, OrderDirection orderDirection)
        {
            SqlConnection dbconnection = new(connectionstring);

            if(orderField == null) 
            {
                orderDirection = OrderDirection.None;
            }

            List<ContentUnit> queryContent = new();
            if(contentType == null) 
            {
                queryContent = dbconnection.Query<ContentUnit>(@$"SELECT * FROM contents ORDER BY ContentId").ToList();
            } 
            else 
            {
                queryContent = dbconnection.Query<ContentUnit>(@$"SELECT * FROM contents WHERE ContentType = '{contentType}' ORDER BY ContentId").ToList();
            }

            List<ContentUnitDTO_output> contentDTO = new();
            Dictionary<int, object> contentFieldFetchers = new();
            Dictionary<int, object> orderedFieldDict = new();

            for (int i = 0; i < queryContent.Count; i++)
            {
                contentDTO.Add(new ContentUnitDTO_output {
                    ContentId = queryContent[i].ContentId,
                    ContentType = queryContent[i].ContentType,
                    ContentBody = JsonConvert.DeserializeObject<Dictionary<string, object>>(queryContent[i].ContentBody),
                    Date = UnixTimeStampToDateTimeConverter.UnixTimeStampToDateTime(queryContent[i].Date)
                });

                if (orderField != null && contentDTO[i].ContentBody.ContainsKey(orderField))
                {
                    contentFieldFetchers.Add(contentDTO[i].ContentId, contentDTO[i].ContentBody[orderField]);
                }
            }

            //ascending
            if(orderDirection == OrderDirection.Ascending) 
            {
                foreach (KeyValuePair<int, object> field in contentFieldFetchers.OrderBy(key => key.Value))
                {
                    orderedFieldDict.Add(field.Key, field.Value);
                }
            }

            //descending
            if(orderDirection == OrderDirection.Descending) 
            {
                foreach (KeyValuePair<int, object> field in contentFieldFetchers.OrderByDescending(key => key.Value))
                {
                    orderedFieldDict.Add(field.Key, field.Value);
                }
            }

            //none
            if(orderDirection == OrderDirection.None)
            {
                //offset process
                contentDTO.RemoveRange(0, offset);

                //limit process
                if(contentDTO.Count >= pageSize) 
                {
                    contentDTO.RemoveRange(pageSize, contentDTO.Count-pageSize);
                }

                return contentDTO;
            }

            List<ContentUnitDTO_output> orderedContentDTO = new();

            int index;
            for (int i = 0; i < orderedFieldDict.Count; i++)
            {
                index = contentDTO.FindIndex(c => c.ContentId == orderedFieldDict.ElementAt(i).Key);
                orderedContentDTO.Add(contentDTO[index]);
            }

            //offset process
            orderedContentDTO.RemoveRange(0, offset);

            //limit process
            if (contentDTO.Count >= pageSize)
            {
                contentDTO.RemoveRange(pageSize, contentDTO.Count - pageSize);
            }

            return orderedContentDTO;
        }

        public bool RemoveContent(int contentId)
        {
            SqlConnection dbconnection = new(connectionstring);

            dbconnection.Execute($"DELETE FROM contents WHERE \"ContentId\" = {contentId}");

            return true;
        }

        public bool UpdateContent(int contentId, string newContentBody)
        {
            SqlConnection dbconnection = new(connectionstring);

            try
            {
                 var jsonString = JsonConvert.DeserializeObject(newContentBody);
            }
            catch
            {
                return false;                
            }

            dbconnection.Execute($"UPDATE contents SET ContentBody = '{newContentBody}' WHERE ContentId = {contentId}");

            return true;
        }
    }
}