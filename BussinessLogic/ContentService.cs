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

        public bool AddContent(string contentType, string contentBody, ClaimsPrincipal user)
        {
            return _contentRepository.AddContent(contentType, contentBody, user);
        }

        public string GetContent(int contentId)
        {
            return _contentRepository.GetContent(contentId);
        }

        public List<ContentUnitDTO_output> GetContents(string contentType, int offset, int pageSize, string orderField, OrderDirection orderDirection)
        {
            return _contentRepository.GetContents(contentType, offset, pageSize, orderField, orderDirection);
        }

        public bool RemoveContent(int contentId)
        {
            return _contentRepository.RemoveContent(contentId);
        }

        public bool UpdateContent(int contentId, string contentBody)
        {
            return _contentRepository.UpdateContent(contentId, contentBody);
        }
    }
}