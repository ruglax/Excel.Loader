using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Labs.Excel.Loader.Model;
using Newtonsoft.Json.Linq;

namespace Labs.Excel.Loader
{
    public interface ILoader
    {
        BufferBlock<Message> BufferBlock { get; }

        void ReadFile();
    }
}