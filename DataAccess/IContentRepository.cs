using System.Collections.Generic;
using FreeCMS.Shared.Entities;

namespace FreeCMS.DataAccess
{
    public interface IContentRepository
    {
        bool AddContent(ContentUnitDTO input);
        bool UpdateContent(int ContentId, ContentUnitDTO input);
        bool RemoveContent(int ContentId);
        List<ContentUnitDTO_output> GetContent(ContentSearchUnit input);
    }
}