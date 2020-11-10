using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Searchfight.Infrastructure.DTOs.Bing;
using Searchfight.Infrastructure.Handler;
using Searchfight.Infrastructure.Services;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Searchfight.Infrastructure.Tests.Services
{
    public class BingSearchEngineGatewayTests
    {
        private readonly Faker _faker;

        public BingSearchEngineGatewayTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void SearchByExpression_ShouldReturnResponse_WhenDataIsAvailable()
        {
            var term = "java";

            var _handler = new Mock<IHttpHandler>();
            var _configuration = IntialMockConfiguration();
            var _gateway = new BingSearchEngineGateway(_configuration.Object, _handler.Object);

            var response = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("{\n \"_Type\": \"CustomSearch\",\n \"WebPages\":{\n \"TotalEstimatedMatches\":1000,\n \"WebSearchUrl\":\"SomeUrl\" \n } \n}")
            };

            var expected = new BingResponseDto
            {
                _Type = "CustomSearch",
                WebPages = new WebPagesDto
                {
                    TotalEstimatedMatches = 1000,
                    WebSearchUrl = "SomeUrl"
                }
            };
            _handler.Setup(s => s.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(response));

            var actual = _gateway.SearchByExpression(term);

            actual.Should().BeEquivalentTo(expected);
        }

        private Mock<IConfiguration> IntialMockConfiguration()
        {
            var _configuration = new Mock<IConfiguration>();

            var _configurationSection = new Mock<IConfigurationSection>();
            _configurationSection.Setup(c => c.Value).Returns(_faker.Lorem.Word());
            _configuration.Setup(x => x.GetSection("BingSearchSettings:Parameters:CustomConfigId")).Returns(_configurationSection.Object);

            var _configurationSection2 = new Mock<IConfigurationSection>();
            _configurationSection2.Setup(c => c.Value).Returns(_faker.Lorem.Word());
            _configuration.Setup(x => x.GetSection("BingSearchSettings:Parameters:SubscriptionKey")).Returns(_configurationSection2.Object);

            var _configurationSection3 = new Mock<IConfigurationSection>();
            _configurationSection3.Setup(c => c.Value).Returns(_faker.Internet.Url());
            _configuration.Setup(x => x.GetSection("BingSearchSettings:BaseAddress")).Returns(_configurationSection3.Object);

            return _configuration;
        }
    }
}
