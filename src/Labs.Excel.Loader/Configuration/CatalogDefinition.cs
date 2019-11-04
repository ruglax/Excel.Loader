using System.Collections.Generic;

namespace Labs.Excel.Loader.Configuration
{
    public class CatalogDefinition
    {
        public string SheetName { get; set; }

        public string EntityName { get; set; }

        public bool Active { get; set; }

        public string Version { get; set; }

        public string Revision { get; set; }

        public List<RowDefinition> Rows { get; set; }
    }
}
