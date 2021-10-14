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
        public List<ContentUnitDTO_output> GetContents(string orderField = "", int offset = 0, int paging = int.MaxValue)
        {
            //for example orderField = age desc
            string orderDirectionStr = orderField.Split(' ')[1];
            string orderFieldName = orderField.Split(' ')[0];
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


            return _contentService.GetContents(offset, paging, orderFieldName, orderDirection);
        }

        [HttpGet("/api/contents/get")]
        public string GetContent(int contentId) 
        {
            return _contentService.GetContent(contentId);
        }

        [HttpPost("/api/contents")]
        public bool AddContents(string contentName, [FromBody] string input)
        {
            return _contentService.AddContent(contentName, input, null);
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