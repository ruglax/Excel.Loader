using System.Collections.Generic;
using System;
using System.IO;
using Labs.Excel.Loader.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Labs.Excel.Loader.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("starting...");
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("catalog.json", optional: true, reloadOnChange: true)
                .Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(configuration, serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            serviceProvider.GetService<ILoader>().ReadFile();

            System.Console.ReadLine();
        }

        private static void ConfigureServices(IConfiguration config, IServiceCollection serviceCollection)
        {
            var catalogConfiguration = new CatalogConfiguration();
            config.Bind(catalogConfiguration); 

            serviceCollection.AddSingleton(config);
            serviceCollection.AddLogging();
            serviceCollection.AddTransient<ISheetReader, SheetReader>();
            serviceCollection.AddTransient<IWorbookReader>(ctx => new WorbookReader(catalogConfiguration));
            serviceCollection.AddTransient<ILoader, Loader>();
        }
    }
}
