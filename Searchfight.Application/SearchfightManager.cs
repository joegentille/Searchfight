using Searchfight.Application.Abstractions;
using Searchfight.Domain.Entities;
using Searchfight.Infrastructure.Services.Abstractions;
using System.Collections.Generic;

namespace Searchfight.Application
{
    public class SearchfightManager : ISearchfightManager
    {
        private readonly IEnumerable<ISearchEngine> _searchEngines;
        private readonly IGoogleSearchEngineGateway _googleGateway;
        private readonly IBingSearchEngineGateway _bingGateway;

        public SearchfightManager(IGoogleSearchEngineGateway googleGateway, IBingSearchEngineGateway bingGateway)
        {
            _googleGateway = googleGateway;
            _bingGateway = bingGateway;

            _searchEngines = new List<ISearchEngine>()
            {
                new GoogleSearchEngine(_googleGateway),
                new BingSearchEngine(_bingGateway)
            };
        }

        public List<SearchResult> SearchEngineResults(string[] terms)
        {
            var searchResults = new List<SearchResult>();

            foreach (var term in terms)
            {
                foreach (var engine in _searchEngines)
                {
                    var result = engine.Search(term);
                    searchResults.Add(result);
                }
            }
            return searchResults;
        }
    }
}
