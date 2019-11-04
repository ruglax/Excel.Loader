using System.Collections.Generic;

namespace Labs.Excel.Loader.Configuration
{
    public class CatalogConfiguration
    {
        public string FilePath { get; set; }

        public List<CatalogDefinition> CatalogDefinition { get; set; } = new List<CatalogDefinition>();
    }
}
