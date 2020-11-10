using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Searchfight.Infrastructure.DTOs.Bing;
using Searchfight.Infrastructure.Handler;
using Searchfight.Infrastructure.Services.Abstractions;
using System;

namespace Searchfight.Infrastructure.Services
{
    public class BingSearchEngineGateway : IBingSearchEngineGateway
    {
        private readonly Uri _baseAddress;
        private readonly string _subscriptionKey;
        private readonly string _customConfigId;

        private readonly IConfiguration _config;
        private readonly IHttpHandler _handler;

        public BingSearchEngineGateway(IConfiguration config, IHttpHandler handler)
        {
            _config = config;
            _handler = handler;

            _baseAddress = new Uri(_config.GetSection("BingSearchSettings:BaseAddress").Value);
            _subscriptionKey = _config.GetSection("BingSearchSettings:Parameters:SubscriptionKey").Value;
            _customConfigId = _config.GetSection("BingSearchSettings:Parameters:CustomConfigId").Value;
        }

        public BingResponseDto SearchByExpression(string searchTerm)
        {
            _handler.AddHeader(_subscriptionKey);

            var query = $"{_baseAddress}search?q={searchTerm}&customconfig={_customConfigId}&mkt=en-US";

            System.Threading.Thread.Sleep(1000);

            var response = _handler.GetAsync(query).Result;

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<BingResponseDto>(content);

            return result;
        }
    }
}
