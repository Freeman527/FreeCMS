using Microsoft.AspNetCore.Mvc;
using FreeCMS.Managers;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeCMS.Entities;

namespace FreeCMS.Controllers 
{
    public class ContentController : Controller 
    {
        [HttpGet("/api/contents")] 
        public string ReadContents(string ContentName)
        {
            return ContentManager.ReadContent(ContentName);
        }

        [HttpPost("/api/contents")] 
        public string AddContents([FromBody] ContentUnitDTO input) 
        {
            return ContentManager.AddContent(input);
        }
    }
}