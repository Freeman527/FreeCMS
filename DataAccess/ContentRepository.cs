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

        public bool AddContent(string contentName, string contentBody, ClaimsPrincipal user)
        {
            SqlConnection dbconnection = new(connectionstring);

            dbconnection.Execute(@$"INSERT INTO contents (ContentName, ContentBody, Date)
                                    VALUES('{contentName}', '{contentBody}', {DateTimeOffset.Now.ToUnixTimeSeconds()})");

            return true;
        }

        public string GetContent(int contentId) 
        {
            SqlConnection dbconnection = new(connectionstring);

            List<ContentUnit> queryContent = dbconnection.Query<ContentUnit>($"SELECT * FROM contents WHERE ContentId = {contentId}").ToList();
            List<ContentUnitDTO_output> contentDTO = new();

            contentDTO.Add(new ContentUnitDTO_output {
                ContentId = queryContent.First().ContentId,
                ContentName = queryContent.First().ContentName,
                ContentBody = JsonConvert.DeserializeObject<Dictionary<string, object>>(queryContent.First().ContentBody)
            });

            return JsonConvert.SerializeObject(contentDTO.First());
        }

        public List<ContentUnitDTO_output> GetContents(int offset = 0, int pageSize = int.MaxValue, string orderField = "", OrderDirection orderDirection = OrderDirection.None)
        {
            SqlConnection dbconnection = new(connectionstring);

            string limitString = $"FETCH NEXT {pageSize} ROWS ONLY";
            if (pageSize <= 0)
            {
                limitString = null;
            }

            List<ContentUnit> queryContent = dbconnection.Query<ContentUnit>($"SELECT * FROM contents ORDER BY ContentId OFFSET {offset} ROWS {limitString}").ToList();
            List<ContentUnitDTO_output> contentDTO = new();
            Dictionary<int, object> contentFieldFetchers = new();
            Dictionary<int, object> orderedFieldDict = new();

            for (int i = 0; i < queryContent.Count; i++)
            {
                contentDTO.Add(new ContentUnitDTO_output {
                    ContentId = queryContent[i].ContentId,
                    ContentName = queryContent[i].ContentName,
                    ContentBody = JsonConvert.DeserializeObject<Dictionary<string, object>>(queryContent[i].ContentBody)
                });

                if (contentDTO[i].ContentBody.ContainsKey(orderField))
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
                return contentDTO;
            }

            List<ContentUnitDTO_output> orderedContentDTO = new();

            int index;
            for (int i = 0; i < orderedFieldDict.Count; i++)
            {
                index = contentDTO.FindIndex(c => c.ContentId == orderedFieldDict.ElementAt(i).Key);
                orderedContentDTO.Add(contentDTO[index]);
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

            dbconnection.Execute($"UPDATE contents SET ContentBody = '{newContentBody}' WHERE ContentId = {contentId}");

            return true;
        }
    }
}