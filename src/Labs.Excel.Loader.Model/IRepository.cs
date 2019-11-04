namespace Labs.Excel.Loader.Model
{
    public interface IRepository<T> where T : class, new()
    {
        void BulkInsert(T[] entities);
    }
}