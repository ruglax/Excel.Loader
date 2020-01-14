using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using Labs.Excel.Loader.Configuration;
using Labs.Excel.Loader.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Labs.Excel.Loader
{
    public class Loader : ILoader
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly ILogger<Loader> _logger;

        //private readonly IWorbookReader _worbookReader;

        private readonly ISheetReaderFactory _sheetReaderFactory;

        private readonly IDictionary<string, BufferBlock<Message>> _bufferBlocks = new Dictionary<string, BufferBlock<Message>>();

        public Loader(IServiceProvider serviceProvider,
            ILogger<Loader> logger,
            //IWorbookReader worbookReader, 
            ISheetReaderFactory sheetReaderFactory)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            //_worbookReader = worbookReader;
            _sheetReaderFactory = sheetReaderFactory;
            ConfigureRepositories();
        }

        public void UploadFile(CatalogFile catalog)
        {
            var worbookReader = new WorbookReader(catalog);
            _logger.LogInformation($"Starting upload processs {worbookReader.CatalogConfiguration.FilePath}", worbookReader);
            var worbook = worbookReader.ReadWorkbook();
            var definitions = worbookReader.CatalogConfiguration.CatalogDefinition.Where(p => p.Active).ToList();
            foreach (var definition in definitions)
            {
                if (definition != null)
                {
                    var bufferName = definition.EntityName ?? definition.SheetName;
                    var sheetReader = _sheetReaderFactory.CreateInstance(worbook, _bufferBlocks[bufferName]);
                    sheetReader.ReadSheet(definition);
                    _bufferBlocks[bufferName].Complete();
                }
            }

            _logger.LogInformation("Reader process completed");
        }

        private void ConfigureRepositories()
        {
            ConfigureRepository<c_Aduana>();
            ConfigureRepository<c_ClaveProdServ>();
            ConfigureRepository<c_ClaveUnidad>();
            ConfigureRepository<c_CodigoPostal>();
            ConfigureRepository<c_FormaPago>();
            ConfigureRepository<c_Impuesto>();
            ConfigureRepository<c_MetodoPago>();
            ConfigureRepository<c_Moneda>();
            ConfigureRepository<c_NumPedimentoAduana>();
            ConfigureRepository<c_Pais>();
            ConfigureRepository<c_PatenteAduanal>();
            ConfigureRepository<c_RegimenFiscal>();
            ConfigureRepository<c_TasaOCuota>();
            ConfigureRepository<c_TipoDeComprobante>();
            ConfigureRepository<c_TipoFactor>();
            ConfigureRepository<c_TipoRelacion>();
            ConfigureRepository<c_UsoCFDI>();

            ConfigureRepository<c_Colonia>();
        }

        private void ConfigureRepository<T>()
            where T : class, new()
        {
            const int batchSize = 10000;
            var consumer = _serviceProvider.GetService<IConsumer>();
            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            var repository = _serviceProvider.GetService<IRepository<T>>();
            ConfigureEntity(consumer.Transform<T>, repository.BulkInsert, batchSize, linkOptions);
        }

        private void ConfigureEntity<T>(Func<Message, T> action, Action<T[]> execution, int batchSize, DataflowLinkOptions linkOptions)
            where T : class
        {
            var transformBlock = new TransformBlock<Message, T>(action);
            var batchBlock = new BatchBlock<T>(batchSize);
            var actionBlock = new ActionBlock<T[]>(m =>
            {
                var temp = m.Where(x => x != null).ToArray();
                var entity = temp.GetType().Name;
                _logger.LogDebug($"Bulk insert {entity} - {temp.Length}");
                execution.Invoke(temp);
                _logger.LogInformation($"Process finished for {entity}");
            });

            batchBlock.LinkTo(actionBlock, linkOptions);
            batchBlock.Completion.ContinueWith(delegate { actionBlock.Complete(); });

            transformBlock.LinkTo(batchBlock, linkOptions);
            transformBlock.Completion.ContinueWith(delegate { batchBlock.Complete(); });

            var bufferBlock = new BufferBlock<Message>();
            bufferBlock.LinkTo(transformBlock, linkOptions, m => m != null && m.Type == typeof(T).Name);
            _bufferBlocks.Add(typeof(T).Name, bufferBlock);
        }
    }
}