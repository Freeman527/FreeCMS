using FreeCMS.Shared.Entities;
using System.Collections.Generic;
using FreeCMS.DataAccess;
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

        public List<ContentUnitDTO_output> GetContents(string contentType, int offset, int pageSize, string rawOrderFieldText)
        {
            //for example orderField = age desc
            string orderFieldName, orderDirectionStr;

            if (rawOrderFieldText == null)
            {
                orderFieldName = null;
                orderDirectionStr = null;
            }
            else
            {
                orderFieldName = rawOrderFieldText.Split(' ')[0];
                orderDirectionStr = rawOrderFieldText.Split(' ')[1];
            }

            OrderDirection orderDirection;

            if (orderDirectionStr == "desc")
            {
                orderDirection = OrderDirection.Descending;
            }
            else if (orderDirectionStr == "asc")
            {
                orderDirection = OrderDirection.Ascending;
            }
            else
            {
                orderDirection = OrderDirection.None;
            }

            return _contentRepository.GetContents(contentType, offset, pageSize, orderFieldName, orderDirection);
        }

        public string RemoveContent(int contentId)
        {
            return _contentRepository.RemoveContent(contentId);
        }

        public string UpdateContent(int contentId, string contentBody)
        {
            return _contentRepository.UpdateContent(contentId, contentBody);
        }
    }
}