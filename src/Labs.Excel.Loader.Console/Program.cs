﻿using System.IO;
using System.Threading.Tasks.Dataflow;
using Labs.Excel.Loader.Configuration;
using Labs.Excel.Loader.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Labs.Excel.Loader.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

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
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(configuration, serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();


            var loader = serviceProvider.GetService<ILoader>();
            

            ConfigureRepository<c_Aduana>(serviceProvider, loader);
            ConfigureRepository<c_ClaveProdServ>(serviceProvider, loader);
            ConfigureRepository<c_ClaveUnidad>(serviceProvider, loader);
            ConfigureRepository<c_CodigoPostal>(serviceProvider, loader);

            loader.UploadFile();

            System.Console.ReadLine();
        }

        private static void ConfigureRepository<T>(ServiceProvider serviceProvider, ILoader loader) 
            where T : class, new()
        {
            const int batchSize = 50000;
            var consumer = serviceProvider.GetService<IConsumer>();
            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            var repository = serviceProvider.GetService<IRepository<T>>();
            loader.ConfigureEntity(consumer.Transform<T>, repository.BulkInsert, batchSize, linkOptions);
        }

        private static void ConfigureServices(IConfiguration config, IServiceCollection serviceCollection)
        {
            var catalogConfiguration = new CatalogConfiguration();
            config.Bind(catalogConfiguration);

            serviceCollection.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddNLog(config);
                //loggingBuilder.AddConsole();
            });

            serviceCollection.AddSingleton(config);
            serviceCollection.AddSingleton(ctx => new ConnectionStringHelper(config.GetConnectionString("DbCatalog")));
            serviceCollection.AddSingleton(ctx => new BufferBlock<Message>(new DataflowBlockOptions
            {
                BoundedCapacity = DataflowBlockOptions.Unbounded,
            }));

            serviceCollection.AddTransient<IWorbookReader>(ctx => new WorbookReader(catalogConfiguration));

            serviceCollection.AddTransient<IRepository<c_Aduana>, Repository<c_Aduana>>();
            serviceCollection.AddTransient<IRepository<c_ClaveProdServ>, Repository<c_ClaveProdServ>>();
            serviceCollection.AddTransient<IRepository<c_ClaveUnidad>, Repository<c_ClaveUnidad>>();
            serviceCollection.AddTransient<IRepository<c_CodigoPostal>, Repository<c_CodigoPostal>>();
            

            serviceCollection.AddTransient<IConsumer, Consumer>();
            serviceCollection.AddTransient<ILoader, Loader>();
        }
    }
}
