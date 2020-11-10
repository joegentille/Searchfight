using FluentAssertions;
using Searchfight.Domain.Entities;
using System.Collections.Generic;
using Xunit;

namespace Searchfight.Application.Tests
{
    public class ProcessReportTests
    {
        private readonly ProcessReport _processReport;

        public ProcessReportTests()
        {
            _processReport = new ProcessReport();
        }

        [Fact]
        public void SearchEngineReport_ShouldReturnFormattedReport_WhenGivenResults()
        {
            var expected = ".net: Google 1500 Bing 800\r\njava: Google 1200 Bing 1000\r\n";
            string[] terms = { ".net", "java" };
            var results = new List<SearchResult>()
            {
                new SearchResult { EngineName = "Google", Result = 1500, TermToSearch = ".net" },
                new SearchResult { EngineName = "Bing", Result = 800, TermToSearch = ".net" },
                new SearchResult { EngineName = "Google", Result = 1200, TermToSearch = "java" },
                new SearchResult { EngineName = "Bing", Result = 1000, TermToSearch = "java" }
            };

            var actual = _processReport.SearchEngineReport(results, terms);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void SearchEngineWinnerReport_ShouldReturnWinnerReport_WhenGivenResults()
        {
            var expected = "Google winner: .net\r\nBing winner: java\r\nTotal winner: .net";
            string[] terms = { ".net", "java" };
            var results = new List<SearchResult>()
            {
                new SearchResult { EngineName = "Google", Result = 1500, TermToSearch = ".net" },
                new SearchResult { EngineName = "Bing", Result = 800, TermToSearch = ".net" },
                new SearchResult { EngineName = "Google", Result = 1200, TermToSearch = "java" },
                new SearchResult { EngineName = "Bing", Result = 1000, TermToSearch = "java" }
            };

            var actual = _processReport.SearchEngineWinnerReport(results, terms);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
