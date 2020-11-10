using FluentAssertions;
using Moq;
using Searchfight.Domain.Entities;
using Searchfight.Infrastructure.DTOs.Bing;
using Searchfight.Infrastructure.Services.Abstractions;
using System;
using Xunit;

namespace Searchfight.Application.Tests
{
    public class BingSearchEngineTests
    {
        private readonly BingSearchEngine _searchEngine;
        private readonly Mock<IBingSearchEngineGateway> _gateway;

        public BingSearchEngineTests()
        {
            _gateway = new Mock<IBingSearchEngineGateway>();
            _searchEngine = new BingSearchEngine(_gateway.Object);
        }

        [Fact]
        public void Search_ShouldReturnSearchReturnModel_WhenGivenATerm()
        {
            var term = "java";
            var expected = new SearchResult
            {
                EngineName = "Bing",
                TermToSearch = term,
                Result = 1000
            };

            var response = new BingResponseDto()
            {
                WebPages = new WebPagesDto { TotalEstimatedMatches = 1000, WebSearchUrl = "http://FakeUrl" },
                _Type = "CustomSearch"
            };

            _gateway.Setup(g => g.SearchByExpression(It.IsAny<string>())).Returns(response);

            var actual = _searchEngine.Search(term);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Search_ShouldThrowException_WhenDataWebPagesIsNull()
        {
            var term = "java";
            var response = new BingResponseDto()
            {
                _Type = "CustomSearch"
            };

            _gateway.Setup(g => g.SearchByExpression(It.IsAny<string>())).Returns(response);

            Action act = () => _searchEngine.Search(term);

            act.Should().Throw<Exception>().And.Message.Should().Be("Requests to the Search Operation API has exceeded rate limit of your current Bing.CustomSearch");
        }
    }
}
