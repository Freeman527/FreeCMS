namespace FreeCMS.Shared.Entities
{
    public class SearchUnit
    {
        public bool SearchAll {get; set;}
        public string ContentName {get; set;}
        public int Offset {get; set;}
        public int Limit {get; set;}
    }
}