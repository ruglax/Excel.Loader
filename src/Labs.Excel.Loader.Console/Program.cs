using System.Collections.Generic;
using System;
using System.IO;
using System.Threading.Tasks.Dataflow;
using Labs.Excel.Loader.Configuration;
using Labs.Excel.Loader.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Labs.Excel.Loader.Database;

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

            var loader = serviceProvider.GetService<ILoader>();
            var consumer = serviceProvider.GetService<IConsumer>();
            //link

            const int batchSize = 10000;
            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

            ConfigureEntity<c_CodigoPostal>(consumer, batchSize, loader, linkOptions);
            ConfigureEntity<c_Aduana>(consumer, batchSize, loader, linkOptions);
            ConfigureEntity<c_ClaveProdServ>(consumer, batchSize, loader, linkOptions);
            ConfigureEntity<c_ClaveUnidad>(consumer, batchSize, loader, linkOptions);

            //loader.BufferBlock.LinkTo(new ActionBlock<Message>(consumer.Transform), linkOptions);

            loader.ReadFile();

            System.Console.ReadLine();
        }

        private static void ConfigureEntity<T>(IConsumer consumer, int batchSize, ILoader loader, DataflowLinkOptions linkOptions)
            where T : class
        {
            var transformBlock = new TransformBlock<Message, T>((Func<Message, T>)consumer.Transform<T>);
            var batchBlock = new BatchBlock<T>(batchSize);
            transformBlock.LinkTo(batchBlock);
            batchBlock.LinkTo(new ActionBlock<T[]>(m =>
            {
                System.Console.WriteLine(m.GetType());
                System.Console.WriteLine(m.Length);
            }));

            loader.BufferBlock.LinkTo(transformBlock, linkOptions, m => m != null && m.Type == typeof(T).Name);
        }

        private static void ConfigureServices(IConfiguration config, IServiceCollection serviceCollection)
        {
            var catalogConfiguration = new CatalogConfiguration();
            config.Bind(catalogConfiguration);

            serviceCollection.AddLogging();
            serviceCollection.AddSingleton(config);
            serviceCollection.AddSingleton(ctx => new BufferBlock<Message>(new DataflowBlockOptions
            {
                BoundedCapacity = DataflowBlockOptions.Unbounded,
            }));

            serviceCollection.AddTransient<IWorbookReader>(ctx => new WorbookReader(catalogConfiguration));
            serviceCollection.AddTransient<IConsumer, Consumer>();
            serviceCollection.AddTransient<ILoader, Loader>();
        }
    }
}
