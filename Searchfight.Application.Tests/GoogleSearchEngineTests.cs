using FluentAssertions;
using Moq;
using Searchfight.Domain.Entities;
using Searchfight.Infrastructure.DTOs.Google;
using Searchfight.Infrastructure.Services.Abstractions;
using System;
using Xunit;

namespace Searchfight.Application.Tests
{
    public class GoogleSearchEngineTests
    {
        private readonly GoogleSearchEngine _searchEngine;
        private readonly Mock<IGoogleSearchEngineGateway> _gateway;

        public GoogleSearchEngineTests()
        {
            _gateway = new Mock<IGoogleSearchEngineGateway>();
            _searchEngine = new GoogleSearchEngine(_gateway.Object);
        }

        [Fact]
        public void Search_ShouldReturnSearchReturnModel_WhenGivenATerm()
        {
            var term = "java";
            var expected = new SearchResult
            {
                EngineName = "Google",
                TermToSearch = term,
                Result = 1500
            };

            var response = new GoogleResponseDto
            {
                Kind = "CustomSearch#Search",
                SearchInformation = new SearchInformationDto { SearchTime = 0.123, TotalResults = "1500" } 
            };

            _gateway.Setup(g => g.SearchByExpression(term)).Returns(response);

            var actual = _searchEngine.Search(term);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Search_ShouldThrowException_WhenResponseIsNull()
        {
            var term = "java";
            GoogleResponseDto response = null;

            _gateway.Setup(g => g.SearchByExpression(It.IsAny<string>())).Returns(response);

            Action act = () => _searchEngine.Search(term);

            act.Should().Throw<Exception>().And.Message.Should().Be("Quota exceeded for quota metric 'Queries' and limit 'Queries per day' of service 'customsearch.googleapis.com'");
        }

    }
}
