using FreeCMS.Shared.Entities;
using System.Collections.Generic;
using FreeCMS.DataAccess;
using System.Linq;

namespace FreeCMS.Managers
{
    public class ContentManager
    {
        private readonly ContentRepository _contentRepository;

        public ContentManager(ContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public bool AddContent(ContentUnitDTO input)
        {
            return _contentRepository.AddContent(input);
        }

        public List<ContentUnitDTO> GetContent(string ContentName)
        {
            return _contentRepository.GetContent(ContentName);
        }

        public bool UpdateContent (ContentUnitDTO input)
        {
            return _contentRepository.UpdateContent(input);
        }

        public bool RemoveContent (int ContentId) 
        {
            return _contentRepository.RemoveContent(ContentId);
        }
    }
}