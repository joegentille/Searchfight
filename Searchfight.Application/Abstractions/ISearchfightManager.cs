using Searchfight.Domain.Entities;
using System.Collections.Generic;

namespace Searchfight.Application.Abstractions
{
    public interface ISearchfightManager
    {
        List<SearchResult> SearchEngineResults(string[] terms);
    }
}
