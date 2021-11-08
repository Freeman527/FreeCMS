using System.Collections.Generic;

namespace FreeCMS.Shared.Entities
{
    public class ContentUnitDTO
    {
        public string ContentName {get; set;}
        public Dictionary<string,string> ContentBody {get; set;}
        public int ContentOwnerId {get; set;}
    }
}