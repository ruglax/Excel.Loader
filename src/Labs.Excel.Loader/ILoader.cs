using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Labs.Excel.Loader.Model;

namespace Labs.Excel.Loader
{
    public interface ILoader
    {
        void ConfigureEntity<T>(Func<Message, T> action, Func<T[], Task> execution, int batchSize, DataflowLinkOptions linkOptions)
            where T : class;


        void UploadFile();
    }
}