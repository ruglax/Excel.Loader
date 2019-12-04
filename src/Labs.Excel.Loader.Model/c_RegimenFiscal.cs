using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Labs.Excel.Loader.Model.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Labs.Excel.Loader.Model
{
    public class c_RegimenFiscal
    {
        [JsonExtensionData]
        private readonly IDictionary<string, JToken> _additionalData;

        public c_RegimenFiscal()
        {
            _additionalData = new Dictionary<string, JToken>();
        }

        public string Clave { get; set; }

        public string Nombre { get; set; }

        public CatalogValidationOptions Fisica { get; set; }

        public CatalogValidationOptions Moral { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Fisica = CatalogValidation.NormalizeCatalogValidation(_additionalData, "FisicaRaw");
            Moral = CatalogValidation.NormalizeCatalogValidation(_additionalData, "MoralRaw");
        }
    }
}
