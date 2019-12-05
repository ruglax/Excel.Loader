using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Labs.Excel.Loader.Configuration;
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;

namespace Labs.Excel.Loader
{
    public class CatalogJsonConverter
    {
        public JToken WriteJson(IRow row, CatalogDefinition catalogDefinition)
        {
            ICell cell = row.GetCell(0);
            var tempCellValue = GetValue(cell)?.ToString();
            if (string.IsNullOrEmpty(tempCellValue))
                return null;

            JTokenWriter writer = new JTokenWriter();
            writer.WriteStartObject();
            foreach (var rowDefinition in catalogDefinition.Columns)
            {
                cell = row.GetCell(rowDefinition.Index);
                var cellValue = GetValue(cell);
                if (cellValue == null && rowDefinition.Index == 0)
                    break;

                writer.WritePropertyName(rowDefinition.PropertyName);
                if (string.IsNullOrWhiteSpace(rowDefinition.Mask))
                {
                    writer.WriteValue(cellValue ?? string.Empty);
                }
                else
                {
                    writer.WriteValue(string.Format($"{{{rowDefinition.Mask}}}", cellValue));
                }

            }

            writer.WriteEndObject();
            return writer.Token;
        }

        private object GetValue(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.String:
                    return cell.ToString();
                case CellType.Numeric:
                    try
                    {
                        string temp = cell.ToString();
                        if (DateTime.TryParseExact(temp, "dd/MM/yyyy", null, DateTimeStyles.None,
                            out DateTime dateTemp))
                        {
                            return dateTemp;
                        }

                        if (DateTime.TryParse(temp, out dateTemp))
                        {
                            return dateTemp;
                        }

                        return cell.NumericCellValue;
                    }
                    catch (Exception)
                    {
                        return cell.NumericCellValue;
                    }
                case CellType.Formula:
                    return cell.StringCellValue;
                default:
                    return cell.ToString();
            }
        }
    }
}
