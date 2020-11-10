using System.Net.Http;
using System.Threading.Tasks;

namespace Searchfight.Infrastructure.Handler
{
    public interface IHttpHandler
    {
        HttpResponseMessage Get(string query);

        Task<HttpResponseMessage> GetAsync(string query);

        void AddHeader(string header);
    }
}
