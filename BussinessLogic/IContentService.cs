using System.Collections.Generic;
using System.Security.Claims;
using FreeCMS.Shared.Entities;

namespace FreeCMS.BussinessLogic
{
    public interface IContentService
    {
        bool AddContent(string contentType, string contentBody, ClaimsPrincipal user);
        bool RemoveContent(int contentId);
        bool UpdateContent(int contentId, string contentBody);
        string GetContent(int contentId);
        List<ContentUnitDTO_output> GetContents(string contentType, int offset = 0, int pageSize = int.MaxValue, string orderField = "");
    }
}