using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using FreeCMS.BussinessLogic;
using System;
using System.Linq;

namespace FreeCMS.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        private readonly IContentService _contentService;

        public ContentController(ContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpPost("/api/v1/content/{contentType}")]
        public IActionResult PostContent(string contentType, [FromBody] string input)
        {
            try
            {
                return Ok(_contentService.PostContent(contentType, input, null));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("/api/v1/contents/{contentType}")]
        public IActionResult GetContents(string contentType, string orderField, int offset, int pageSize = int.MaxValue)
        {
            try
            {
                return Ok(_contentService.GetContents(contentType, offset, pageSize, orderField).Select(x => x.ContentBody));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("/api/v1/content/{contentId}")]
        public IActionResult GetContent(int contentId)
        {
            try
            {
                return Ok(_contentService.GetContent(contentId).ContentBody);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("/api/v1/content/{contentId}/{contentType}")]
        public IActionResult PutContent(int contentId, [FromBody] string newBody)
        {
            try
            {
                return Ok(_contentService.PutContent(contentId, newBody));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("/api/v1/content/{contentId}")]
        public IActionResult RemoveContent(int contentId)
        {
            try
            {
                return Ok(_contentService.RemoveContent(contentId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}