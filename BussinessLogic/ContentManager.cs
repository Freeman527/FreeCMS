using FreeCMS.Shared.Entities;
using System.Collections.Generic;
using FreeCMS.DataAccess;
using System.Linq;
using System.Security.Claims;

namespace FreeCMS.BussinessLogic
{
    public class ContentService : IContentService
    {
        private readonly ContentRepository _contentRepository;

        public ContentService(ContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public bool AddContent(string contentName, string contentBody, ClaimsPrincipal user)
        {
            throw new System.NotImplementedException();
        }

        public string GetContent(int contentId)
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetContents(int offset = 0, int pageSize = int.MaxValue, string orderField = "", OrderDirection orderDirection = OrderDirection.None)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveContent(int contentId)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateContent(int contentId, string contentBody)
        {
            throw new System.NotImplementedException();
        }
    }
}