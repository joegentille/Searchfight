using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Searchfight.ConsoleApp.Module;
using System;
using System.IO;
using System.Linq;

namespace Searchfight.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if(!args.Any())
            {
                Console.WriteLine("Please, deliver at least one value to search");
                Console.ReadLine();
                return;
            }

            var serviceCollection = Configure();
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            try
            {
                var service = serviceProvider.GetService<SearchfightProcess>();
                var result = service.Execute(args);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"We can not process now, please try again later. See details: {ex}");
            }
            Console.ReadLine();
        }

        private static IServiceCollection Configure()
        {
            var serviceCollection = new ServiceCollection();
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", false, reloadOnChange: true);
            var config = builder.Build();

            serviceCollection.AddSingleton<IConfiguration>(config);
            serviceCollection.AddApplicationServices();
            return serviceCollection;
        }
    }
}
