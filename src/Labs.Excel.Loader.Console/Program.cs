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
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Extensions;

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

            ConfigureRepository<c_Aduana>(serviceProvider, loader, consumer);
            ConfigureRepository<c_ClaveProdServ>(serviceProvider, loader, consumer);
            ConfigureRepository<c_ClaveUnidad>(serviceProvider, loader, consumer);
            ConfigureRepository<c_CodigoPostal>(serviceProvider, loader, consumer);
            loader.ReadFile();

            System.Console.ReadLine();
        }

        private static void ConfigureRepository<T>(ServiceProvider serviceProvider, ILoader loader, IConsumer consumer) 
            where T : class, new()
        {
            const int batchSize = 50000;
            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            var aduanaRepository = serviceProvider.GetService<IRepository<T>>();
            loader.ConfigureEntity(consumer.Transform<T>, aduanaRepository.BulkInsert, batchSize, linkOptions);
        }

        private static void ConfigureServices(IConfiguration config, IServiceCollection serviceCollection)
        {
            var catalogConfiguration = new CatalogConfiguration();
            config.Bind(catalogConfiguration);

            serviceCollection.AddLogging();
            serviceCollection.AddDbContext<DbCatalogContext>(options =>
            {
                options.UseSqlServer("server=.\\STAMPING;database=DBCATALOGOSv4;trusted_connection=true;User Id=sa;Password=123;");
            });
            //TODO : Move to factory
            EntityFrameworkManager.ContextFactory = context =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<DbCatalogContext>();
                optionsBuilder.UseSqlServer("server=.\\STAMPING;database=DBCATALOGOSv4;trusted_connection=true;User Id=sa;Password=123;");
                return new DbCatalogContext(optionsBuilder.Options);
            };

            serviceCollection.AddSingleton(config);
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
