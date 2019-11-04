using System.Threading.Tasks.Dataflow;
using Labs.Excel.Loader.Model;
using NPOI.SS.UserModel;

namespace Labs.Excel.Loader
{
    public interface ISheetReaderFactory
    {
        SheetReader CreateInstance(IWorkbook worbook, BufferBlock<Message> bufferBlock);
    }
}