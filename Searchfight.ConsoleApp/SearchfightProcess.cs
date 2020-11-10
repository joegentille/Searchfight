using Searchfight.Application.Abstractions;
using System.Linq;

namespace Searchfight.ConsoleApp
{
    public class SearchfightProcess
    {
        private readonly ISearchfightManager _manager;
        private readonly IProcessReport _processReport;

        public SearchfightProcess(ISearchfightManager manager, IProcessReport processReport)
        {
            _manager = manager;
            _processReport = processReport;
        }

        public string Execute(string[] args)
        {
            var results = _manager.SearchEngineResults(args);
            if(!results.Any())
            {
                return "No data available to process";
            }

            var engineReport = _processReport.SearchEngineReport(results, args);
            var winnerReport = _processReport.SearchEngineWinnerReport(results, args);

            return engineReport + winnerReport;
        }
    }
}
