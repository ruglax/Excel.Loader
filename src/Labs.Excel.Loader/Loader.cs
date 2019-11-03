using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Labs.Excel.Loader.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Labs.Excel.Loader
{
    public class Loader : ILoader
    {
        private readonly ILogger<Loader> _logger;

        private readonly IWorbookReader _worbookReader;

        private readonly BufferBlock<Message> _bufferBlock;

        public Loader(ILogger<Loader> logger, IWorbookReader worbookReader, BufferBlock<Message> bufferBlock)
        {
            _logger = logger;
            _worbookReader = worbookReader;
            _bufferBlock = bufferBlock;
        }

        public BufferBlock<Message> BufferBlock => _bufferBlock;

        public void UploadFile()
        {
            var worbook = _worbookReader.ReadWorkbook();
            var definitions = _worbookReader.CatalogConfiguration.CatalogDefinition.Where(p => p.Active).ToList();
            //Parallel.ForEach(definitions, definition =>
            //{
            //    if (definition != null)
            //    {
            //        var sheetReader = new SheetReader(worbook, _bufferBlock);
            //        sheetReader.ReadSheet(definition);
            //    }
            //});
            foreach (var definition in definitions)
            {
                if (definition != null)
                {
                    var sheetReader = new SheetReader(worbook, _bufferBlock);
                    sheetReader.ReadSheet(definition);
                }
            }

            _bufferBlock.Complete();
            _logger.LogInformation("Process completed");
        }

        public void ConfigureEntity<T>(Func<Message, T> action, Action<T[]> execution, int batchSize, DataflowLinkOptions linkOptions)
            where T : class
        {
            var transformBlock = new TransformBlock<Message, T>(action);
            var batchBlock = new BatchBlock<T>(batchSize);
            var actionBlock = new ActionBlock<T[]>(m =>
            {
                var temp = m.Where(x => x != null).ToArray();
                _logger.LogDebug($"Bulk insert {temp.GetType().Name} - {temp.Length}");
                execution.Invoke(temp);
            });

            batchBlock.LinkTo(actionBlock, linkOptions);
            batchBlock.Completion.ContinueWith(delegate { actionBlock.Complete(); });

            transformBlock.LinkTo(batchBlock, linkOptions);
            transformBlock.Completion.ContinueWith(delegate { batchBlock.Complete(); });

            BufferBlock.LinkTo(transformBlock, linkOptions, m => m != null && m.Type == typeof(T).Name);
        }
    }
}
