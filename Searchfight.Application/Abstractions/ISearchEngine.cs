using Searchfight.Domain.Entities;

namespace Searchfight.Application.Abstractions
{
    public interface ISearchEngine
    {
        SearchResult Search(string term);
    }
}
