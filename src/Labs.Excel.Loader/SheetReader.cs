using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Labs.Excel.Loader.Configuration;
using Labs.Excel.Loader.Model;
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;

namespace Labs.Excel.Loader
{
    public class SheetReader : ISheetReader
    {
        private readonly IWorkbook _workbook;

        private readonly ITargetBlock<Message> _targetBlock;

        private readonly CatalogDefinition _catalogDefinition;

        private bool _startProcess;

        public SheetReader(IWorkbook workbook, ITargetBlock<Message> targetBlock, CatalogDefinition catalogDefinition)
        {
            _workbook = workbook;
            _targetBlock = targetBlock;
            _catalogDefinition = catalogDefinition;
        }

        public void ReadSheet(CatalogDefinition catalogDefinition)
        {
            var sheets = _catalogDefinition?.SheetName?.Split(',');
            if (sheets != null)
            {
                ReadSheet(catalogDefinition, sheet.Trim());
            }
        }

        private void ReadSheet(CatalogDefinition catalogDefinition, string sheetName)
        {
            var sheet = _workbook.GetSheet(sheetName);
            if (sheet == null) return;

            RowDefinition clave = _catalogDefinition.Rows
                .OrderBy(p => p.Index)
                .FirstOrDefault();

            int records = 0;
            for (int rowIndex = 0; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                try
                {
                    var row = sheet.GetRow(rowIndex);
                    if (row == null)
                        continue;

                    var tempValue = row.GetCell(0)?.ToString();
                    if (!_startProcess && tempValue == clave?.Name)
                    {
                        _startProcess = true;
                        continue;
                    }

                    if (_startProcess)
                    {
                        var jtoken = WriteJson(row, _catalogDefinition);
                        if (jtoken != null)
                        {
                            _targetBlock.Post(new Message
                            {
                                RecordIndex = records,
                                Type = _catalogDefinition.EntityName ?? _catalogDefinition.SheetName,
                                JToken = jtoken
                            });

                            records++;
                        }
                    }
                }
                catch (Exception)
                {
                    //Console.WriteLine("----------------------");
                    //Console.WriteLine($"sheet: {sheet}, rowIndex: {rowIndex}");
                    //Console.WriteLine(e);
                    //Console.WriteLine("----------------------");
                }
            }
        }

        private JToken WriteJson(IRow row, CatalogDefinition catalogDefinition)
        {
            JTokenWriter writer = new JTokenWriter();
            writer.WriteStartObject();
            foreach (var rowDefinition in catalogDefinition.Rows)
            {
                ICell cell = row.GetCell(rowDefinition.Index);
                //if (cell.CellType == CellType.Blank && !rowDefinition.Nullable)
                //{
                //    return null;
                //}

                writer.WritePropertyName(rowDefinition.PropertyName);
                if (string.IsNullOrWhiteSpace(rowDefinition.Mask))
                {
                    writer.WriteValue(cell?.ToString() ?? string.Empty);
                }
                else
                {
                    writer.WriteValue(string.Format($"{{{rowDefinition.Mask}}}", GetValue(cell)));
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
                    return cell.NumericCellValue;
                default:
                    return cell.ToString();
            }
        }
    }
}
