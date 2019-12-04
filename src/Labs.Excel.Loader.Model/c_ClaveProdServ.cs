using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Labs.Excel.Loader.Model.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Labs.Excel.Loader.Model
{
    public partial class c_ClaveProdServ
    {
        [JsonExtensionData]
        private readonly IDictionary<string, JToken> _additionalData;

        public c_ClaveProdServ()
        {
            _additionalData = new Dictionary<string, JToken>();
        }

        public string Id { get; set; }

        public string Descripcion { get; set; }

        [JsonIgnore]
        public CatalogValidationOptions IncluirIVA { get; set; }

        [JsonIgnore]
        public CatalogValidationOptions IncluirIEPS { get; set; }

        public short EstimuloFranjaFronteriza { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            IncluirIVA = CatalogValidation.NormalizeCatalogValidation(_additionalData, "IncluirIVA");
            IncluirIEPS = CatalogValidation.NormalizeCatalogValidation(_additionalData, "IncluirIEPS");
        }
    }
}
