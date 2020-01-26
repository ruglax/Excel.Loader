using System;
using System.Collections.Generic;
using System.Text;

namespace Labs.Excel.Loader.Configuration
{
    public class CatalogFile
    {
        public string FilePath { get; set; }

        public bool Active { get; set; }

        public List<CatalogDefinition> CatalogDefinition { get; set; } = new List<CatalogDefinition>();
    }
}
