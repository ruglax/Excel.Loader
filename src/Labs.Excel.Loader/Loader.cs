using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Labs.Excel.Loader.Model;
using Newtonsoft.Json.Linq;

namespace Labs.Excel.Loader
{
    public class Loader : ILoader
    {
        private readonly IWorbookReader _worbookReader;

        private readonly BufferBlock<Message> _bufferBlock;

        public Loader(IWorbookReader worbookReader, BufferBlock<Message> bufferBlock)
        {
            _worbookReader = worbookReader;
            _bufferBlock = bufferBlock;
        }

        public BufferBlock<Message> BufferBlock => _bufferBlock;

        public void ReadFile()
        {
            var worbook = _worbookReader.ReadWorkbook();
            var activedefinictio = _worbookReader.CatalogConfiguration.CatalogDefinition.Where(p => p.Active).ToList();
            Parallel.ForEach(activedefinictio, async definition =>
            {
                if (definition != null)
                {
                    var sheetReader = new SheetReader(worbook, _bufferBlock, definition);
                    await sheetReader.ReadSheetAsync();
                }
            });

            _bufferBlock.Complete();
        }

        public void ConfigureEntity<T>(Func<Message, T> action, Action<T[]> execution, int batchSize, DataflowLinkOptions linkOptions)
            where T : class
        {
            var transformBlock = new TransformBlock<Message, T>(action);
            var batchBlock = new BatchBlock<T>(batchSize);
            //var actionBlock = new ActionBlock<T[]>(execution.Invoke);
            var actionBlock = new ActionBlock<T[]>(m =>
            {
                Console.WriteLine($"Bulk insert {m.GetType().Name} - {m.Length}");
                execution.Invoke(m);
            });

            batchBlock.LinkTo(actionBlock, linkOptions);
            batchBlock.Completion.ContinueWith(delegate { actionBlock.Complete(); });

            transformBlock.LinkTo(batchBlock, linkOptions);
            transformBlock.Completion.ContinueWith(delegate { batchBlock.Complete(); });

            BufferBlock.LinkTo(transformBlock, linkOptions, m => m != null && m.Type == typeof(T).Name);
        }
    }
}
