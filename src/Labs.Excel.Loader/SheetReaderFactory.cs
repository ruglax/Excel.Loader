using System.Threading.Tasks.Dataflow;
using Labs.Excel.Loader.Model;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;

namespace Labs.Excel.Loader
{
    public class SheetReaderFactory : ISheetReaderFactory
    {
        private readonly ILogger<SheetReader> _logger;

        public SheetReaderFactory(ILogger<SheetReader> logger)
        {
            _logger = logger;
        }

        public SheetReader CreateInstance(IWorkbook worbook, BufferBlock<Message> bufferBlock)
        {
            return new SheetReader(_logger, worbook, bufferBlock);
        }
    }
}
