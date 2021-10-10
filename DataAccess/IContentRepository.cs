using System.Collections.Generic;
using FreeCMS.Shared.Entities;

namespace FreeCMS.DataAccess
{
    public interface IContentRepository
    {
        bool AddContent(string contentBody, string contentName, int ownerId);
        bool UpdateContent(int contentId, string newContentBody);
        bool RemoveContent(int ContentId);
        List<ContentUnitDTO_output> GetContent(ContentSearchUnit input);
    }
}