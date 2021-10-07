using FreeCMS.Shared.Entities;
using System.Collections.Generic;
using FreeCMS.DataAccess;
using System.Linq;

namespace FreeCMS.Managers
{
    public class ContentManager : IContentRepository
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

        public List<ContentUnitDTO_output> GetContent(ContentSearchUnit input)
        {
            //temporary solution
            var content = _contentRepository.GetContent(input);
            if(content == null) 
            {
                List<ContentUnitDTO_output> response = new();
                response.Add(new ContentUnitDTO_output 
                {
                    ContentName = "Wrong content name"
                }); 

                return response;
            }
            //

            return _contentRepository.GetContent(input);
        }

        public bool UpdateContent (int ContentId, ContentUnitDTO input)
        {
            return _contentRepository.UpdateContent(ContentId, input);
        }

        public bool RemoveContent (int ContentId) 
        {
            return _contentRepository.RemoveContent(ContentId);
        }
    }
}