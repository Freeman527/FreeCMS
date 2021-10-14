using System;
using System.Collections.Generic;

namespace FreeCMS.Shared.Entities
{
    public class ContentUnitDTO_output
    {
        public int ContentId {get; set;}
        public string ContentType {get; set;}
        public Dictionary<string,object> ContentBody {get; set;}
        public string ContentOwner {get; set;}
        public DateTime Date {get; set;}
    }
}