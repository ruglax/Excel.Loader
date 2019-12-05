using System;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using Labs.Excel.Loader.Model;
using Microsoft.Extensions.Logging;

namespace Labs.Excel.Loader
{
    public class Loader : ILoader
    {
        private readonly ILogger<Loader> _logger;

        private readonly IWorbookReader _worbookReader;

        private readonly ISheetReaderFactory _sheetReaderFactory;

        public Loader(ILogger<Loader> logger, IWorbookReader worbookReader, ISheetReaderFactory sheetReaderFactory, BufferBlock<Message> bufferBlock)
        {
            _logger = logger;
            _worbookReader = worbookReader;
            _sheetReaderFactory = sheetReaderFactory;
            BufferBlock = bufferBlock;
        }

        public BufferBlock<Message> BufferBlock { get; }

        public void UploadFile()
        {
            _logger.LogInformation($"Starting upload processs {_worbookReader.CatalogConfiguration.FilePath}", _worbookReader);
            var worbook = _worbookReader.ReadWorkbook();
            var definitions = _worbookReader.CatalogConfiguration.CatalogDefinition.Where(p => p.Active).ToList();
            foreach (var definition in definitions)
            {
                if (definition != null)
                {
                    var sheetReader = _sheetReaderFactory.CreateInstance(worbook, BufferBlock);
                    sheetReader.ReadSheet(definition);
                }
            }

            BufferBlock.Complete();
            _logger.LogInformation("Reader process completed");
        }

        public void ConfigureEntity<T>(Func<Message, T> action, Action<T[]> execution, int batchSize, DataflowLinkOptions linkOptions)
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

            BufferBlock.LinkTo(transformBlock, linkOptions, m => m != null && m.Type == typeof(T).Name);
        }
    }
}