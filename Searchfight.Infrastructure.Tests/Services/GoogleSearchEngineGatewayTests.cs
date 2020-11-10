using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Searchfight.Infrastructure.DTOs.Google;
using Searchfight.Infrastructure.Handler;
using Searchfight.Infrastructure.Services;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Searchfight.Infrastructure.Tests.Services
{
    public class GoogleSearchEngineGatewayTests
    {
        private readonly Faker _faker;

        public GoogleSearchEngineGatewayTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void SearchByExpression_ShouldReturnResponse_WhenDataIsAvailable()
        {
            var term = "java";

            var _handler = new Mock<IHttpHandler>();
            var _configuration = IntialMockConfiguration();
            var _gateway = new GoogleSearchEngineGateway(_configuration.Object, _handler.Object);

            var response = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("{\n \"kind\": \"Custom\",\n \"searchInformation\":{\n \"totalResults\":\"1500\",\n \"searchTime\":0.123 \n } \n}")
            };

            var expected = new GoogleResponseDto
            {
                Kind = "Custom",
                SearchInformation = new SearchInformationDto
                {
                    SearchTime = 0.123,
                    TotalResults = "1500"
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
            _configuration.Setup(x => x.GetSection("GoogleSearchSettings:Parameters:ApiKey")).Returns(_configurationSection.Object);

            var _configurationSection2 = new Mock<IConfigurationSection>();
            _configurationSection2.Setup(c => c.Value).Returns(_faker.Lorem.Word());
            _configuration.Setup(x => x.GetSection("GoogleSearchSettings:Parameters:CX")).Returns(_configurationSection2.Object);

            var _configurationSection3 = new Mock<IConfigurationSection>();
            _configurationSection3.Setup(c => c.Value).Returns(_faker.Internet.Url());
            _configuration.Setup(x => x.GetSection("GoogleSearchSettings:BaseAddress")).Returns(_configurationSection3.Object);

            return _configuration;
        }

    }
}
