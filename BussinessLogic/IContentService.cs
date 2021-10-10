using System.Collections.Generic;
using System.Security.Claims;
using FreeCMS.Shared.Entities;

namespace FreeCMS.BussinessLogic
{
    public interface IContentService
    {
        bool AddContent(string contentName, string contentBody, ClaimsPrincipal user);
        bool RemoveContent(int contentId);
        bool UpdateContent(int contentId, string contentBody);
        List<string> GetContents(int offset = 0, int pageSize = int.MaxValue, string orderField = "", OrderDirection orderDirection = OrderDirection.None);
        string GetContent(int contentId);
    }
}