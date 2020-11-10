using Searchfight.Domain.Entities;
using System.Collections.Generic;

namespace Searchfight.Application.Abstractions
{
    public interface IProcessReport
    {
        string SearchEngineReport(List<SearchResult> results, string[] terms);

        string SearchEngineWinnerReport(List<SearchResult> results, string[] terms);
    }
}
