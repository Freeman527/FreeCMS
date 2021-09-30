using System;
using System.Collections.Generic;

namespace FreeCMS.Entities
{
    public class ContentUnitDTO
    {
        public int ContentId {get; set;}
        public string ContentName {get; set;}
        public Dictionary<string,string> ContentBody {get; set;}
        public int ContentOwnerId {get; set;}
    }
}