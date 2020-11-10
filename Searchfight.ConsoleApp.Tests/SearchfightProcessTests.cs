using FluentAssertions;
using Moq;
using Searchfight.Application.Abstractions;
using Searchfight.Domain.Entities;
using System.Collections.Generic;
using Xunit;

namespace Searchfight.ConsoleApp.Tests
{
    public class SearchfightProcessTests
    {
        private readonly Mock<ISearchfightManager> _manager;
        private readonly Mock<IProcessReport> _report;
        private readonly SearchfightProcess _process;

        public SearchfightProcessTests()
        {
            _manager = new Mock<ISearchfightManager>();
            _report = new Mock<IProcessReport>();
            _process = new SearchfightProcess(_manager.Object, _report.Object);
        }

        [Fact]
        public void Execute_ShouldReturnReport_WhenHavingResults()
        {
            string[] args = { ".net", "java" };
            var results = new List<SearchResult>()
            {
                new SearchResult { EngineName = "Google", Result = 1500, TermToSearch = ".net" },
                new SearchResult { EngineName = "Bing", Result = 800, TermToSearch = ".net" },
                new SearchResult { EngineName = "Google", Result = 1200, TermToSearch = "java" },
                new SearchResult { EngineName = "Bing", Result = 1000, TermToSearch = "java" }
            };

            _manager.Setup(s => s.SearchEngineResults(args)).Returns(results);
            _report.Setup(s => s.SearchEngineReport(results, args)).Returns("SomeString");
            _report.Setup(s => s.SearchEngineWinnerReport(results, args)).Returns("SomeWinnerString");

            var actual = _process.Execute(args);

            actual.Should().NotBeEmpty();
        }

        [Fact]
        public void Execute_ShouldReturnEmptyReport_WhenNotHavingResults()
        {
            string[] args = { ".net", "java" };
            var expected = "No data available to process";
            var results = new List<SearchResult>();

            _manager.Setup(s => s.SearchEngineResults(args)).Returns(results);

            var actual = _process.Execute(args);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
