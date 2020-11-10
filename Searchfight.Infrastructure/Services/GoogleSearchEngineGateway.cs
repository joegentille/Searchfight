using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Searchfight.Infrastructure.DTOs.Google;
using Searchfight.Infrastructure.Handler;
using Searchfight.Infrastructure.Services.Abstractions;
using System;

namespace Searchfight.Infrastructure.Services
{
    public class GoogleSearchEngineGateway : IGoogleSearchEngineGateway
    {
        private readonly Uri _baseAddress;
        private readonly string _apiKey;
        private readonly string _cx;
        private readonly IConfiguration _config;
        private readonly IHttpHandler _handler;

        public GoogleSearchEngineGateway(IConfiguration config, IHttpHandler handler)
        {
            _config = config;
            _handler = handler;
            
            _apiKey = _config.GetSection("GoogleSearchSettings:Parameters:ApiKey").Value;
            _cx = _config.GetSection("GoogleSearchSettings:Parameters:CX").Value;
            _baseAddress = new Uri(_config.GetSection("GoogleSearchSettings:BaseAddress").Value);
        }

        public GoogleResponseDto SearchByExpression(string searchTerm)
        {
            var query = $"{_baseAddress}v1?key={_apiKey}&cx={_cx}&q={searchTerm}";

            var response = _handler.GetAsync(query).Result;

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<GoogleResponseDto>(content);

            return result;
        }
    }
}
