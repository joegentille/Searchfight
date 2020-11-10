using FluentAssertions;
using Searchfight.Application.Mappers;
using Searchfight.Domain.Entities;
using Searchfight.Infrastructure.DTOs.Bing;
using Searchfight.Infrastructure.DTOs.Google;
using Xunit;

namespace Searchfight.Application.Tests.Mappers
{
    public class SearchResultMapperTests
    {
        [Fact]
        public void ToSearchResultModel_ShouldMapTotalEstimatedMatchesToResult_WhenProcessingBingResponse()
        {
            var term = ".net";
            var engine = "Bing";
            var bingResponseDto = new BingResponseDto
            {
                WebPages = new WebPagesDto { TotalEstimatedMatches = 1500 }
            };

            var expected = new SearchResult
            {
                EngineName = engine,
                Result = 1500,
                TermToSearch = term
            };

            var actual = bingResponseDto.ToSearchResultModel(term, engine);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ToSearchResultModel_ShouldMapSearchInformationTotalResultsToResult_WhenProcessingGoogleResponse()
        {
            var term = ".net";
            var engine = "Google";
            var bingResponseDto = new GoogleResponseDto
            {
                SearchInformation = new SearchInformationDto { TotalResults = "1000" }
            };

            var expected = new SearchResult
            {
                EngineName = engine,
                Result = 1000,
                TermToSearch = term
            };

            var actual = bingResponseDto.ToSearchResultModel(term, engine);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
