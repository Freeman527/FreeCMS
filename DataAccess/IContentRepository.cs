using System.Collections.Generic;
using System.Security.Claims;
using FreeCMS.Shared.Entities;

namespace FreeCMS.DataAccess
{
    public interface IContentRepository
    {
        bool PostContent(string contentType, string contentBody, ClaimsPrincipal user);
        string PutContent(int contentId, string newContentBody);
        string RemoveContent(int contentId);
        string GetContent(int contentId);
        List<ContentUnitDTO_output> GetContents(string contentType, int offset = 0, int pageSize = int.MaxValue, string orderField = "", OrderDirection orderDirection = OrderDirection.None);
    }
}