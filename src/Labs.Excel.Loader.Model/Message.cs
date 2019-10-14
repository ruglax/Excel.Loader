using Newtonsoft.Json.Linq;

namespace Labs.Excel.Loader.Model
{
    public class Message
    {
        public string Type { get; set; }

        public int RecordIndex { get; set; }

        public JToken JToken { get; set; }
    }
}
