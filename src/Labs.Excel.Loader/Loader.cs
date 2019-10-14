using System.Linq;
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
        }


    }
}
