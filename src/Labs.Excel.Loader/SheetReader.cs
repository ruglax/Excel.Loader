using System;
using System.Globalization;
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

        private readonly CatalogJsonConverter _catalogJsonConverter;

        private bool _startProcess;

        public SheetReader(ILogger<SheetReader> logger, IWorkbook workbook, ITargetBlock<Message> targetBlock)
        {
            _logger = logger;
            _workbook = workbook;
            _targetBlock = targetBlock;
            _catalogJsonConverter = new CatalogJsonConverter();
        }

        public void ReadSheet(CatalogDefinition catalogDefinition)
        {
            _logger.LogInformation($"Reading configuration {catalogDefinition.SheetName}", catalogDefinition);
            string[] sheets = catalogDefinition.SheetName.Split(',');
            sheets.ToList().ForEach(sheet => ReadSheet(catalogDefinition, sheet.Trim()));
        }

        private void ReadSheet(CatalogDefinition catalogDefinition, string sheetName)
        {
            _logger.LogInformation($"Reading sheet {sheetName}");
            var sheet = _workbook.GetSheet(sheetName);
            if (sheet == null) return;

            ColumnDefinition clave = catalogDefinition.Columns
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

                    var tempCellValue = row.GetCell(0)?.ToString();
                    if (!_startProcess && tempCellValue?.ToUpper() == clave?.Name?.ToUpper())
                    {
                        _startProcess = true;
                        continue;
                    }

                    if (_startProcess && catalogDefinition.ExcludedValues.All(p => p as string != tempCellValue))
                    {
                        var jtoken = _catalogJsonConverter.WriteJson(row, catalogDefinition);
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
    }
}
