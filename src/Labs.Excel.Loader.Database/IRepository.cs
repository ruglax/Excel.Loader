namespace Labs.Excel.Loader.Database
{
    public interface IRepository<T> where T : class, new()
    {
        void BulkInsert(T[] entities);
    }
}