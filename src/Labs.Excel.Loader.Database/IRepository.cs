using System.Threading.Tasks;

namespace Labs.Excel.Loader.Database
{
    public interface IRepository<T> where T : class, new()
    {
        Task BulkInsert(T[] entities);
    }
}