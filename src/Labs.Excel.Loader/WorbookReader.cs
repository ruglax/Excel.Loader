using System.IO;
using Labs.Excel.Loader.Configuration;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Labs.Excel.Loader
{
    public class WorbookReader : IWorbookReader
    {
        private readonly CatalogFile _configuration;

        public WorbookReader(CatalogFile configuration)
        {
            _configuration = configuration;
        }

        public CatalogFile CatalogConfiguration => _configuration;

        public IWorkbook ReadWorkbook()
        {
            IWorkbook xssfwb;
            using (FileStream fs = new FileStream(_configuration.FilePath, FileMode.Open, FileAccess.Read))
            {
                xssfwb = new XSSFWorkbook(fs);
            }

            return xssfwb;
        }
    }
}
