using Microsoft.AspNetCore.Mvc;
using FreeCMS.Managers;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeCMS.Entities;

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
        public List<ContentUnitDTO> ReadContents(string ContentName)
        {
            return _contentManager.ReadContent(ContentName);
        }

        [HttpPost("/api/contents")] 
        public string AddContents([FromBody] ContentUnitDTO input) 
        {
            return _contentManager.AddContent(input);
        }

        [HttpPut("/api/contents")] 
        public string UpdateContents([FromBody] ContentUnitDTO input) 
        {
            return _contentManager.UpdateContent(input);
        }

        [HttpDelete("/api/contents")]
        public string DeleteContent(int ContentId) 
        {
            return _contentManager.DeleteContent(ContentId);
        } 
    }
}