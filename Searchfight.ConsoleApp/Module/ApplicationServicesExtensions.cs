using Microsoft.Extensions.DependencyInjection;
using Searchfight.Application;
using Searchfight.Application.Abstractions;
using Searchfight.Infrastructure.Handler;
using Searchfight.Infrastructure.Services;
using Searchfight.Infrastructure.Services.Abstractions;

namespace Searchfight.ConsoleApp.Module
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<SearchfightProcess>();
            services.AddScoped<IProcessReport, ProcessReport>();
            services.AddScoped<ISearchfightManager, SearchfightManager>();
            services.AddTransient<ISearchEngine, GoogleSearchEngine>();
            services.AddTransient<ISearchEngine, BingSearchEngine>();
            services.AddTransient<IBingSearchEngineGateway, BingSearchEngineGateway>();
            services.AddTransient<IGoogleSearchEngineGateway, GoogleSearchEngineGateway>();
            services.AddTransient<IHttpHandler, HttpHandler>();
            return services;
        }
    }
}
