using System;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using Labs.Excel.Loader.Configuration;
using Labs.Excel.Loader.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;

namespace Labs.Excel.Loader
{
    public class SheetReader : ISheetReader
    {
        private readonly ILogger<SheetReader> _logger;

        private readonly IWorkbook _workbook;

        private readonly ITargetBlock<Message> _targetBlock;

        private bool _startProcess;

        public SheetReader(ILogger<SheetReader> logger, IWorkbook workbook, ITargetBlock<Message> targetBlock)
        {
            _logger = logger;
            _workbook = workbook;
            _targetBlock = targetBlock;
        }

        public void ReadSheet(CatalogDefinition catalogDefinition)
        {
            _logger.LogInformation($"Reading configuration {catalogDefinition.SheetName}", catalogDefinition);
            var sheets = catalogDefinition.SheetName.Split(',');
            foreach (var sheet in sheets)
            {

                ReadSheet(catalogDefinition, sheet.Trim());
            }
        }

        private void ReadSheet(CatalogDefinition catalogDefinition, string sheetName)
        {
            _logger.LogInformation($"Reading sheet {sheetName}");
            var sheet = _workbook.GetSheet(sheetName);
            if (sheet == null) return;

            RowDefinition clave = catalogDefinition.Rows
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

                    var temp = row.GetCell(0)?.ToString();
                    if (!_startProcess && temp == clave?.Name)
                    {
                        _startProcess = true;
                        continue;
                    }

                    if (_startProcess)
                    {
                        var jtoken = WriteJson(row, catalogDefinition);
                        if (jtoken != null)
                        {
                            var message = new Message
                            {
                                RecordIndex = records,
                                Type = catalogDefinition.EntityName ?? catalogDefinition.SheetName,
                                JToken = jtoken
                            };

                            _logger.LogDebug($"Sending message {rowIndex} of type {message.Type} to bufferBlock", message);
                            _targetBlock.Post(message);
                            records++;
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogDebug(e, "Error al convertir a json", rowIndex);
                }
            }

            _logger.LogInformation($"Founded records {sheet.SheetName}: {records}");
        }

        private JToken WriteJson(IRow row, CatalogDefinition catalogDefinition)
        {
            JTokenWriter writer = new JTokenWriter();
            writer.WriteStartObject();
            foreach (var rowDefinition in catalogDefinition.Rows)
            {
                ICell cell = row.GetCell(rowDefinition.Index);
                writer.WritePropertyName(rowDefinition.PropertyName);
                if (string.IsNullOrWhiteSpace(rowDefinition.Mask))
                {
                    writer.WriteValue(GetValue(cell) ?? string.Empty);
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
                    try
                    {
                        return cell.DateCellValue;
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
