using System.Threading.Tasks;

namespace Labs.Excel.Loader
{
    public interface ISheetReader
    {
        Task ReadSheetAsync();
    }
}