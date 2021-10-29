using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using FreeCMS.BussinessLogic;
using System;

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

        [HttpPost("/api/content/add")]
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
        [HttpGet("/api/contents/get/{contentType}")]
        public IActionResult GetContents(string contentType, string orderField, int offset, int pageSize = int.MaxValue)
        {
            try
            {
                return Ok(_contentService.GetContents(contentType, offset, pageSize, orderField));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("/api/content/get")]
        public IActionResult GetContent(int contentId)
        {
            try
            {
                return Ok(_contentService.GetContent(contentId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("/api/content/update")]
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

        [HttpDelete("/api/content/delete")]
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