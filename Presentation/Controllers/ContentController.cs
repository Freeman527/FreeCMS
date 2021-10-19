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

        public ContentController(ContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet("/api/contents/get/{contentType}")]
        public IActionResult GetContents(string contentType, string orderField, int offset, int paging = int.MaxValue)
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

            return Ok(_contentService.GetContents(contentType, offset, paging, orderFieldName, orderDirection));
        }

        [HttpGet("/api/content/get")]
        public string GetContent(int contentId) 
        {
            return _contentService.GetContent(contentId);
        }

        [HttpPost("/api/content/add")]
        public bool AddContents(string contentType, [FromBody] string input)
        {
            return _contentService.AddContent(contentType, input, null);
        }

        [HttpPut("/api/content/update")]
        public bool UpdateContents(int contentId, [FromBody] string newBody)
        {
            return _contentService.UpdateContent(contentId, newBody);
        }

        [HttpDelete("/api/content/delete")]
        public bool RemoveContent(int contentId)
        {
            return _contentService.RemoveContent(contentId);
        }
    }
}