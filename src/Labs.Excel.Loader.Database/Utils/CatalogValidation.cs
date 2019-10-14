using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Labs.Excel.Loader.Database.Utils
{
    public static class CatalogValidation
    {
        public static CatalogValidationOptions NormalizeCatalogValidation(IDictionary<string, JToken> additionalData, string property)
        {
            string incluirIVA = additionalData.ContainsKey(property) ? (string)additionalData[property] : string.Empty;
            incluirIVA = incluirIVA
                .Trim()
                .ToUpper()
                .Replace("Í", "I");

            switch (incluirIVA)
            {
                case "SI":
                    return CatalogValidationOptions.Obligatorio;
                case "OPCIONAL":
                    return CatalogValidationOptions.Opcional;
                default:
                    return CatalogValidationOptions.NoExistir;
            }
        }
    }
}
