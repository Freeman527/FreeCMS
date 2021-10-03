using Microsoft.AspNetCore.Mvc;
using FreeCMS.Managers;
using FreeCMS.Shared.Entities;
using System.Collections.Generic;

namespace FreeCMS.Controllers 
{
    public class ContentController : Controller 
    {
        private readonly ContentManager _contentManager;

        public ContentController(ContentManager contentManager)
        {
            _contentManager = contentManager;    
        }

        [HttpGet("/api/contents")] 
        public List<ContentUnitDTO> GetContents(string ContentName)
        {
            return _contentManager.GetContent(ContentName);
        }

        [HttpPost("/api/contents")] 
        public bool AddContents([FromBody] ContentUnitDTO input) 
        {
            return _contentManager.AddContent(input);
        }

        [HttpPut("/api/contents")] 
        public bool UpdateContents([FromBody] ContentUnitDTO input) 
        {
            return _contentManager.UpdateContent(input);
        }

        [HttpDelete("/api/contents")]
        public bool RemoveContent(int ContentId) 
        {
            return _contentManager.RemoveContent(ContentId);
        } 
    }
}