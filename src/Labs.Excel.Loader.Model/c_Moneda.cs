using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Labs.Excel.Loader.Model.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Labs.Excel.Loader.Model
{
    public partial class c_Moneda
    {
        [JsonExtensionData]
        private readonly IDictionary<string, JToken> _additionalData;

        public c_Moneda()
        {
            _additionalData = new Dictionary<string, JToken>();
        }

        public string Clave { get; set; }

        public string Nombre { get; set; }

        public short Decimales { get; set; }

        public short? PorcentajeVariacion { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            string value = Regex.Replace((string)_additionalData["PorcentajeVariacionRaw"], @"\D", string.Empty);
            if (!string.IsNullOrEmpty(value))
            {
                PorcentajeVariacion = short.Parse(value);
            }
        }
    }
}
