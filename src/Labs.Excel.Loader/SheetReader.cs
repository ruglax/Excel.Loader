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

        public async Task ReadSheetAsync()
        {
            var sheets = _catalogDefinition.SheetName.Split(',');
            foreach (var sheet in sheets)
            {
                await ReadSheet(sheet.Trim());
            }
        }

        private async Task ReadSheet(string sheetName)
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

                    var temp = row.GetCell(0)?.ToString();
                    if (!_startProcess && temp == clave?.Name)
                    {
                        _startProcess = true;
                        continue;
                    }

                    if (_startProcess)
                    {
                        var jtoken = WriteJson(row);
                        await _targetBlock.SendAsync(new Message
                        {
                            RecordIndex = records,
                            Type = _catalogDefinition.EntityName ?? _catalogDefinition.SheetName,
                            JToken = jtoken
                        });

                        records++;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("----------------------");
                    Console.WriteLine($"sheet: {sheet}, rowIndex: {rowIndex}");
                    Console.WriteLine(e);
                    Console.WriteLine("----------------------");
                }
            }

            Console.WriteLine($"Records procesados {sheet.SheetName}: {records}");
        }

        private JToken WriteJson(IRow row)
        {
            JTokenWriter writer = new JTokenWriter();
            writer.WriteStartObject();
            foreach (var rowDefinition in _catalogDefinition.Rows)
            {
                ICell cell = row.GetCell(rowDefinition.Index);
                writer.WritePropertyName(rowDefinition.PropertyName);
                writer.WriteValue(cell?.ToString() ?? string.Empty);
            }

            writer.WriteEndObject();
            return writer.Token;
        }
    }
}
