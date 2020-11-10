using Searchfight.Infrastructure.DTOs.Bing;
using System.Threading.Tasks;

namespace Searchfight.Infrastructure.Services.Abstractions
{
    public interface IBingSearchEngineGateway
    {
        BingResponseDto SearchByExpression(string searchTerm);
    }
}
