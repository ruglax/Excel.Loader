namespace Labs.Excel.Loader.Database
{
    public interface IIndexHelper
    {
        dynamic GetClusteredIndex(string table);
        void DeleteIndex(string table, string index, string column);
        void CreateIndex(string table, string index, string column);
    }
}