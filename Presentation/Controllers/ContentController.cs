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

        [HttpPost("/api/contents/get")] 
        public List<ContentUnitDTO_output> GetContents([FromBody] SearchUnit input)
        {
            return _contentManager.GetContent(input);
        }

        [HttpPost("/api/contents")] 
        public bool AddContents([FromBody] ContentUnitDTO input) 
        {
            return _contentManager.AddContent(input);
        }

        [HttpPut("/api/contents")] 
        public bool UpdateContents(int ContentId, [FromBody] ContentUnitDTO input) 
        {
            return _contentManager.UpdateContent(ContentId ,input);
        }

        [HttpDelete("/api/contents")]
        public bool RemoveContent(int ContentId) 
        {
            return _contentManager.RemoveContent(ContentId);
        } 
    }
}