using FluentAssertions;
using Moq;
using Searchfight.Domain.Entities;
using Searchfight.Infrastructure.DTOs.Bing;
using Searchfight.Infrastructure.DTOs.Google;
using Searchfight.Infrastructure.Services.Abstractions;
using System.Collections.Generic;
using Xunit;

namespace Searchfight.Application.Tests
{
    public class SearchfightManagerTests
    {
        private readonly SearchfightManager _manager;
        private readonly Mock<IGoogleSearchEngineGateway> _googleGateway;
        private readonly Mock<IBingSearchEngineGateway> _bingGateway;

        public SearchfightManagerTests()
        {
            _googleGateway = new Mock<IGoogleSearchEngineGateway>();
            _bingGateway = new Mock<IBingSearchEngineGateway>();
            _manager = new SearchfightManager(_googleGateway.Object, _bingGateway.Object);
        }

        [Fact]
        public void SearchEngineResults_ShouldReturnResults_WhenGivenValidTerms()
        {
            string[] terms = { ".net", "java" };
            var expected = new List<SearchResult>()
            {
                new SearchResult { EngineName = "Google", Result = 1500, TermToSearch = ".net" },
                new SearchResult { EngineName = "Bing", Result = 1000, TermToSearch = ".net" },
                new SearchResult { EngineName = "Google", Result = 1500, TermToSearch = "java" },
                new SearchResult { EngineName = "Bing", Result = 1000, TermToSearch = "java" }
            };

            var bingResponse = new BingResponseDto
            {
                WebPages = new WebPagesDto { TotalEstimatedMatches = 1000, WebSearchUrl = "SomeUrl" },
                _Type = "Custom"
            };

            var googleResponse = new GoogleResponseDto
            {
                Kind = "CustomSerch",
                SearchInformation = new SearchInformationDto { SearchTime = 0.123, TotalResults = "1500" }
            };

            _bingGateway.Setup(s => s.SearchByExpression(".net")).Returns(bingResponse);
            _bingGateway.Setup(s => s.SearchByExpression("java")).Returns(bingResponse);

            _googleGateway.Setup(s => s.SearchByExpression(".net")).Returns(googleResponse);
            _googleGateway.Setup(s => s.SearchByExpression("java")).Returns(googleResponse);

            var actual = _manager.SearchEngineResults(terms);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
