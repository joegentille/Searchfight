using Searchfight.Application.Abstractions;
using Searchfight.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Searchfight.Application
{
    public class ProcessReport : IProcessReport
    {
        public string SearchEngineReport(List<SearchResult> results, string[] terms)
        {
            string report = string.Empty;
            foreach (var term in terms)
            {
                report = report + $"{term}:";
                var grouped = results.Where(d => d.TermToSearch.ToLower() == term.ToLower()).Select(s => s).ToList();
                foreach (var result in grouped)
                {
                    report = report + $" {result.EngineName} {result.Result}";
                }
                report = report + Environment.NewLine;
            }
            return report;
        }

        public string SearchEngineWinnerReport(List<SearchResult> results, string[] terms)
        {
            string report = string.Empty;

            var grouped = results.GroupBy(s => s.EngineName).ToList();
            foreach (var item in grouped)
            {
                report = report + $"{item.Key} winner: {item.OrderByDescending(s => s.Result).Select(s => s.TermToSearch).FirstOrDefault()}";
                report = report + Environment.NewLine;
            }
            var totalWinner = results.OrderByDescending(s => s.Result).Select(s => s.TermToSearch).FirstOrDefault();
            report = report + "Total winner: " + totalWinner;
            return report;
        }
    }
}
