using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Labs.Excel.Loader.Model.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Labs.Excel.Loader.Model
{
    public class c_TasaOCuota
    {
        [JsonExtensionData]
        private readonly IDictionary<string, JToken> _additionalData;

        public c_TasaOCuota()
        {
            _additionalData = new Dictionary<string, JToken>();
        }

        public int Id { get; set; }

        public TasaOCuoutaType TasaOCuotaType { get; set; }

        public string ValorMinimo { get; set; }

        public string ValorMaximo { get; set; }

        /// <summary>
        /// Referencia al catálogo c_Impuesto
        /// </summary>
        public string Impuesto { get; set; }

        /// <summary>
        /// Referencia al catálogo c_TipoFactor
        /// </summary>
        public string Factor { get; set; }

        public CatalogValidationOptions Traslado { get; set; }

        public CatalogValidationOptions Retencion { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            SetTasaOCuotaType();
            SetImpuesto();
            Traslado = CatalogValidation.NormalizeCatalogValidation(_additionalData, "TrasladoRaw");
            Retencion = CatalogValidation.NormalizeCatalogValidation(_additionalData, "RetencionRaw");
        }

        private void SetTasaOCuotaType()
        {
            string tasaOCuotatypeRaw = (string) _additionalData["TasaOCuotaTypeRaw"];
            if (Enum.TryParse<TasaOCuoutaType>(tasaOCuotatypeRaw, out var tasaOCuotaType))
            {
                TasaOCuotaType = tasaOCuotaType;
            }
        }

        private void SetImpuesto()
        {
            string impuestoRaw = (string) _additionalData["ImpuestoRaw"];
            var impuestos = new List<string> {"IVA", "IEPS", "ISR"};
            if (impuestos.Any(p => p == impuestoRaw))
            {
                Impuesto = impuestoRaw;
            }
            else
            {
                foreach (var temp in impuestos)
                {
                    if (impuestoRaw.Contains(temp))
                    {
                        Impuesto = temp;
                    }
                }
            }
        }
    }

    public enum TasaOCuoutaType
    {
        Fijo = 1,

        Rango = 2
    }
}
