using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Searchfight.Infrastructure.Handler
{
    public class HttpHandler : IHttpHandler
    {
        private HttpClient _client;

        public HttpHandler()
        {
            _client = new HttpClient();
        }

        public void AddHeader(string header)
        {
            _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", header);
        }

        public HttpResponseMessage Get(string query)
        {
            return GetAsync(query).Result;
        }

        public async Task<HttpResponseMessage> GetAsync(string query)
        {
            try
            {
                var result = await _client.GetAsync(query);
                if(result == null)
                {
                    throw new NullReferenceException();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
