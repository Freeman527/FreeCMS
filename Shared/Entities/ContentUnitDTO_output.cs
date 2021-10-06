using System.Collections.Generic;

namespace FreeCMS.Shared.Entities
{
    public class ContentUnitDTO_output
    {
        public int ContentId {get; set;}
        public string ContentName {get; set;}
        public Dictionary<string,string> ContentBody {get; set;}
        public string ContentOwner {get; set;}
    }
}