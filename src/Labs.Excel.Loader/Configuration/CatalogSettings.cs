﻿using System.Collections.Generic;

namespace Labs.Excel.Loader.Configuration
{
    public class CatalogSettings
    {
        public string Version { get; set; }

        public List<CatalogFile> Files = new List<CatalogFile>();
    }
}
