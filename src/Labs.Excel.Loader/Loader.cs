using System.Linq;
using System.Threading.Tasks;

namespace Labs.Excel.Loader
{
    public class Loader : ILoader
    {
        private readonly IWorbookReader _worbookReader;

        public Loader(IWorbookReader worbookReader)
        {
            _worbookReader = worbookReader;
        }

        public void ReadFile()
        {
            var worbook = _worbookReader.ReadWorkbook();
            var activedefinictio = _worbookReader.CatalogConfiguration.CatalogDefinition.Where(p => p.Active).ToList();
            Parallel.ForEach(activedefinictio, def =>
            {
                if (def != null)
                    new SheetReader(worbook, def).ReadSheet();
            });
        }


    }
}
