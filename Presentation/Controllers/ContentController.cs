using Microsoft.AspNetCore.Mvc;
using FreeCMS.Managers;
using FreeCMS.Shared.Entities;
using System.Collections.Generic;
using FreeCMS.BussinessLogic;
using FreeCMS.DataAccess;

namespace FreeCMS.Controllers
{
    public class ContentController : Controller
    {
        private readonly IContentService _contentService;

        public ContentController(ContentService contentManager)
        {
            _contentService = contentManager;
        }

        [HttpGet("/api/contents/gets")]
        public List<ContentUnitDTO_output> GetContents(string contentType, string orderField, int offset, int paging)
        {
            //for example orderField = age desc
            string orderFieldName, orderDirectionStr;

            if (orderField == null)
            {
                orderFieldName = null;
                orderDirectionStr = null;
            }
            else
            {
                orderFieldName = orderField.Split(' ')[0];
                orderDirectionStr = orderField.Split(' ')[1];
            }

            var orderDirection = OrderDirection.None;

            if (orderDirectionStr == "desc")
            {
                orderDirection = OrderDirection.Descending;
            }
            else if (orderDirectionStr == "asc")
            {
                orderDirection = OrderDirection.Ascending;
            }
            else if(orderDirectionStr == "null") 
            {
                orderDirection = OrderDirection.None;
            }

            return _contentService.GetContents(contentType, offset, paging, orderFieldName, orderDirection);
        }

        [HttpGet("/api/contents/get")]
        public string GetContent(int contentId) 
        {
            return _contentService.GetContent(contentId);
        }

        [HttpPost("/api/contents")]
        public bool AddContents(string contentType, [FromBody] string input)
        {
            return _contentService.AddContent(contentType, input, null);
        }

        [HttpPut("/api/contents")]
        public bool UpdateContents(int contentId, [FromBody] string newBody)
        {
            return _contentService.UpdateContent(contentId, newBody);
        }

        [HttpDelete("/api/contents")]
        public bool RemoveContent(int contentId)
        {
            return _contentService.RemoveContent(contentId);
        }
    }
}