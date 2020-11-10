using Searchfight.Application.Abstractions;
using Searchfight.Application.Mappers;
using Searchfight.Domain.Entities;
using Searchfight.Infrastructure.Services.Abstractions;
using System;

namespace Searchfight.Application
{
    public class BingSearchEngine : ISearchEngine
    {
        private readonly IBingSearchEngineGateway _gateway;
        private const string engine = "Bing";

        public BingSearchEngine(IBingSearchEngineGateway gateway)
        {
            _gateway = gateway;
        }

        public SearchResult Search(string term)
        {
            var data = _gateway.SearchByExpression(term);

            if (data.WebPages == null)
            {
                throw new Exception("Requests to the Search Operation API has exceeded rate limit of your current Bing.CustomSearch");
            }            

            var searchResult = data.ToSearchResultModel(term, engine);

            return searchResult;
        }
    }
}
