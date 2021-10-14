using System.Collections.Generic;
using System.Security.Claims;
using FreeCMS.Shared.Entities;

namespace FreeCMS.DataAccess
{
    public interface IContentRepository
    {
        bool AddContent(string contentName, string contentBody, ClaimsPrincipal user);
        bool UpdateContent(int contentId, string newContentBody);
        bool RemoveContent(int contentId);
        string GetContent(int contentId);
        List<ContentUnitDTO_output> GetContents(int offset = 0, int pageSize = int.MaxValue, string orderField = "", OrderDirection orderDirection = OrderDirection.None);
    }
}