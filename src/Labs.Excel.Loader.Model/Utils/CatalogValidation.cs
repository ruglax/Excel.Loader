using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Labs.Excel.Loader.Model.Utils
{
    public static class CatalogValidation
    {
        public static CatalogValidationOptions NormalizeCatalogValidation(IDictionary<string, JToken> additionalData, string property)
        {
            string normalized = additionalData.ContainsKey(property) ? (string)additionalData[property] : string.Empty;
            normalized = normalized
                .Trim()
                .ToUpper()
                .Replace("Í", "I");

            switch (normalized)
            {
                case "SI":
                    return CatalogValidationOptions.SI;
                case "OPCIONAL":
                    return CatalogValidationOptions.OPCIONAL;
                case "NO":
                    return CatalogValidationOptions.NO;
                default:
                    return CatalogValidationOptions.UNDEFINED;
            }
        }
    }
}
