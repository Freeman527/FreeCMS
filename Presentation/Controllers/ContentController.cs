using Microsoft.AspNetCore.Mvc;
using FreeCMS.Managers;
using FreeCMS.Shared.Entities;
using System.Collections.Generic;
using FreeCMS.BussinessLogic;

namespace FreeCMS.Controllers
{
    public class ContentController : Controller
    {
        private readonly IContentService _contentService;

        public ContentController(ContentService contentManager)
        {
            _contentService = contentManager;
        }

        [HttpGet("/api/contents/get")]
        public List<string> GetContents(string orderField = "", int offset = 0, int paging = int.MaxValue)
        {
            //for example orderField = age desc
            string orderDirectionStr = orderField.Split(' ')[1];
            var orderDirection = OrderDirection.None;

            if (orderDirectionStr == "desc")
            {
                orderDirection = OrderDirection.Descending;
            }
            else if (orderDirectionStr == "asc")
            {
                orderDirection = OrderDirection.Ascending;
            }

            return _contentService.GetContents(offset, paging, orderField, orderDirection);
        }

        [HttpPost("/api/contents")]
        public bool AddContents(string contentName, [FromBody] string input)
        {
            return _contentService.AddContent(contentName, input, null);
        }

        [HttpPut("/api/contents")]
        public bool UpdateContents(int ContentId, [FromBody] string newBody)
        {
            return _contentService.UpdateContent(ContentId, newBody);
        }

        [HttpDelete("/api/contents")]
        public bool RemoveContent(int ContentId)
        {
            return _contentService.RemoveContent(ContentId);
        }
    }
}