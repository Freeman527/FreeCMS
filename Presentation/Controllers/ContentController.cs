using Microsoft.AspNetCore.Mvc;
using FreeCMS.Managers;
using FreeCMS.Shared.Entities;
using System.Collections.Generic;
using FreeCMS.BussinessLogic;
using FreeCMS.DataAccess;
using System;

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

        [HttpGet("/api/content/get")]
        public string GetContent(int contentId) 
        {
            return _contentService.GetContent(contentId);
        }

        [HttpPost("/api/content/add")]
        public IActionResult AddContents(string contentType, [FromBody] string input)
        {
            try
            {
                return Ok(_contentService.AddContent(contentType, input, null));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("/api/content/update")]
        public IActionResult UpdateContents(int contentId, [FromBody] string newBody)
        {
            try
            {
                return Ok(_contentService.UpdateContent(contentId, newBody));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("/api/content/delete")]
        public bool RemoveContent(int contentId)
        {
            return _contentService.RemoveContent(contentId);
        }
    }
}