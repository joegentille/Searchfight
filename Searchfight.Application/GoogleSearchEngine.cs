using Searchfight.Application.Abstractions;
using Searchfight.Application.Mappers;
using Searchfight.Domain.Entities;
using Searchfight.Infrastructure.Services.Abstractions;
using System;

namespace Searchfight.Application
{
    public class GoogleSearchEngine : ISearchEngine
    {
        private readonly IGoogleSearchEngineGateway _gateway;
        private const string engine = "Google";

        public GoogleSearchEngine(IGoogleSearchEngineGateway gateway)
        {
            _gateway = gateway;
        }

        public SearchResult Search(string term)
        {
            var data = _gateway.SearchByExpression(term);

            if(data == null || data.SearchInformation == null)
            {
                throw new Exception("Quota exceeded for quota metric 'Queries' and limit 'Queries per day' of service 'customsearch.googleapis.com'");
            }

            var searchResult = data.ToSearchResultModel(term, engine);

            return searchResult;
        }
    }
}
