using Searchfight.Infrastructure.DTOs.Google;
using System.Threading.Tasks;

namespace Searchfight.Infrastructure.Services.Abstractions
{
    public interface IGoogleSearchEngineGateway
    {
        GoogleResponseDto SearchByExpression(string searchTerm);
    }
}
