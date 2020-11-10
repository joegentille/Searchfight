using Searchfight.Domain.Entities;
using Searchfight.Infrastructure.DTOs.Bing;
using Searchfight.Infrastructure.DTOs.Google;
using System;

namespace Searchfight.Application.Mappers
{
    public static class SearchResultMapper
    {
        public static SearchResult ToSearchResultModel(this BingResponseDto response, string term, string engine)
        {
            var bingResult = new SearchResult();
            bingResult.Result = response.WebPages.TotalEstimatedMatches;
            bingResult.EngineName = engine;
            bingResult.TermToSearch = term;
            return bingResult;
        }

        public static SearchResult ToSearchResultModel(this GoogleResponseDto response, string term, string engine)
        {
            var googleResult = new SearchResult();
            googleResult.Result = Convert.ToInt64(response.SearchInformation.TotalResults);
            googleResult.EngineName = engine;
            googleResult.TermToSearch = term;
            return googleResult;
        }
    }
}
