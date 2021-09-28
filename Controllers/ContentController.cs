using Microsoft.AspNetCore.Mvc;
using FreeCMS.Managers;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeCMS.Entities;

namespace FreeCMS.Controllers 
{
    public class ContentController : Controller 
    {
        private ContentManager _contentManager;

        public ContentController(ContentManager contentManager)
        {
            _contentManager = contentManager;    
        }

        [HttpGet("/api/contents")] 
        public ContentUnitDTO ReadContents(string ContentName)
        {
            return _contentManager.ReadContent(ContentName);
        }

        [HttpPost("/api/contents")] 
        public string AddContents([FromBody] ContentUnitDTO input) 
        {
            return _contentManager.AddContent(input);
        }
    }
}