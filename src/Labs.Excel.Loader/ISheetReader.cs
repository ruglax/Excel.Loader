using Labs.Excel.Loader.Configuration;

namespace Labs.Excel.Loader
{
    public interface ISheetReader
    {
        void ReadSheet(CatalogDefinition catalogDefinition);
    }
}