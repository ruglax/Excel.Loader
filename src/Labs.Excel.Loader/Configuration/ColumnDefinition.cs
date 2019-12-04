using System.Collections.Generic;

namespace Labs.Excel.Loader.Configuration
{
    public class ColumnDefinition
    {
        public short Index { get; set; }

        public string Name { get; set; }

        public string PropertyName { get; set; }

        public string Mask { get; set; }
    }
}
