using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using Labs.Excel.Loader.Configuration;
using Labs.Excel.Loader.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Labs.Excel.Loader.Database;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog.Extensions.Logging;

namespace Labs.Excel.Loader.Console
{
    class Program
    {
        private static CatalogSettings CatalogSettings = new CatalogSettings();

        static void Main(string[] args)
        {
            System.Console.WriteLine("starting...");
            string directory = Directory.GetCurrentDirectory();
            CatalogSettings = LoadJson($"{directory}\\catalog.json");
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(configuration, serviceCollection);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            foreach (var file in CatalogSettings.Files.Where(p => p.Active))
            {
                serviceProvider.GetService<ILoader>().UploadFile(file);
            }

            System.Console.ReadLine();
        }

        private static CatalogSettings LoadJson(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<CatalogSettings>(json);
            }
        }

        private static void ConfigureServices(IConfiguration config, IServiceCollection serviceCollection)
        {

            serviceCollection.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddNLog(config);
            });

            serviceCollection.AddSingleton(config);
            serviceCollection.AddSingleton(ctx => new ConnectionStringHelper(config.GetConnectionString("DbCatalog")));
            serviceCollection.AddSingleton(ctx => new BufferBlock<Message>(new DataflowBlockOptions
            {
                BoundedCapacity = DataflowBlockOptions.Unbounded,
            }));

            serviceCollection.AddTransient<IIndexHelper, IndexHelper>();
            serviceCollection.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            serviceCollection.AddTransient<ISheetReaderFactory, SheetReaderFactory>();
            serviceCollection.AddTransient<IConsumer, Consumer>();
            serviceCollection.AddTransient<ILoader, Loader>();
        }
    }
}
