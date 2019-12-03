using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;
using Labs.Excel.Loader.Model.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Labs.Excel.Loader.Model
{
    public class c_FormaPago
    {
        [JsonExtensionData]
        private readonly IDictionary<string, JToken> _additionalData;

        public c_FormaPago()
        {
            _additionalData = new Dictionary<string, JToken>();
        }

        public string Clave { get; set; }

        public string Nombre { get; set; }

        public CatalogValidationOptions Bancarizado { get; set; }

        public CatalogValidationOptions NumeroOperacion { get; set; }

        public CatalogValidationOptions RfcEmisorCuentaOrdenante { get; set; }

        public CatalogValidationOptions CuentaOrdenante { get; set; }

        public string PatronCuentaOrdenante { get; set; }

        public CatalogValidationOptions RfcEmisorCuentaBeneficiario { get; set; }

        public CatalogValidationOptions CuentaBeneficiario { get; set; }

        public string PatronCuentaBeneficiaria { get; set; }

        public CatalogValidationOptions TipoCadenaPago { get; set; }

        public CatalogValidationOptions NombreBancoEmisorCuentaOrdenante { get; set; }

        public string NombreBancoEmisorCuentaOrdenanteRule { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Bancarizado = CatalogValidation.NormalizeCatalogValidation(_additionalData, "BancarizadoRaw");
            NumeroOperacion = CatalogValidation.NormalizeCatalogValidation(_additionalData, "NumeroOperacionRaw");
            RfcEmisorCuentaOrdenante = CatalogValidation.NormalizeCatalogValidation(_additionalData, "RfcEmisorCuentaOrdenanteRaw");
            CuentaOrdenante = CatalogValidation.NormalizeCatalogValidation(_additionalData, "CuentaOrdenanteRaw");
            PatronCuentaOrdenante = CatalogValidation.NormalizeCatalogValidation(_additionalData, "PatronCuentaOrdenanteRaw") == CatalogValidationOptions.UNDEFINED
                ? (string)_additionalData["PatronCuentaOrdenanteRaw"] : string.Empty;
            RfcEmisorCuentaBeneficiario = CatalogValidation.NormalizeCatalogValidation(_additionalData, "RfcEmisorCuentaBeneficiarioRaw");
            CuentaBeneficiario = CatalogValidation.NormalizeCatalogValidation(_additionalData, "CuentaBeneficiarioRaw");
            PatronCuentaBeneficiaria = CatalogValidation.NormalizeCatalogValidation(_additionalData, "PatronCuentaBeneficiariaRaw") == CatalogValidationOptions.UNDEFINED
                ? (string)_additionalData["PatronCuentaBeneficiariaRaw"] : string.Empty;
            TipoCadenaPago = CatalogValidation.NormalizeCatalogValidation(_additionalData, "TipoCadenaPagoRaw");
            var tempNombreBancoEmisorCuentaOrdenante =
                CatalogValidation.NormalizeCatalogValidation(_additionalData, "NombreBancoEmisorCuentaOrdenanteRaw");
            NombreBancoEmisorCuentaOrdenante = tempNombreBancoEmisorCuentaOrdenante == CatalogValidationOptions.UNDEFINED
                ? CatalogValidationOptions.REGLAVALIDACION : tempNombreBancoEmisorCuentaOrdenante;
            NombreBancoEmisorCuentaOrdenanteRule = tempNombreBancoEmisorCuentaOrdenante == CatalogValidationOptions.UNDEFINED ?
                (string)_additionalData["NombreBancoEmisorCuentaOrdenanteRaw"] : string.Empty;
        }
    }
}
