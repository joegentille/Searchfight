namespace Searchfight.Domain.Entities
{
    public class SearchResult
    {
        public string EngineName { get; set; }

        public string TermToSearch { get; set; }

        public long Result { get; set; }
    }
}
