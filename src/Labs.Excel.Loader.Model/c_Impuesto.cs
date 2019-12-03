using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Labs.Excel.Loader.Model.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Labs.Excel.Loader.Model
{
    public class c_Impuesto
    {
        [JsonExtensionData]
        private readonly IDictionary<string, JToken> _additionalData;

        public c_Impuesto()
        {
            _additionalData = new Dictionary<string, JToken>();
        }

        public string Clave { get; set; }

        public string Nombre { get; set; }

        public CatalogValidationOptions Retencion { get; set; }

        public CatalogValidationOptions Traslado { get; set; }

        public string LocalOFederal { get; set; }

        public string Entidad { get; set; }

        public DateTime FechaInicio { get; set; } = new DateTime(2017, 01, 01);

        public DateTime? FechaFin { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Retencion = CatalogValidation.NormalizeCatalogValidation(_additionalData, "RetencionRaw");
            Traslado = CatalogValidation.NormalizeCatalogValidation(_additionalData, "TrasladoRaw");
        }
    }
}
