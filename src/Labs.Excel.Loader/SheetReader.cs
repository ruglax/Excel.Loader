using System;
using Labs.Excel.Loader.Configuration;
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;

namespace Labs.Excel.Loader
{
    public class SheetReader : ISheetReader
    {
        private readonly IWorkbook _workbook;

        private readonly CatalogDefinition _catalogDefinition;

        private bool _startProcess;

        public SheetReader(IWorkbook workbook, CatalogDefinition catalogDefinition)
        {
            _workbook = workbook;
            _catalogDefinition = catalogDefinition;
        }

        public void ReadSheet()
        {
            var sheet = _workbook.GetSheet(_catalogDefinition.SheetName);
            if (sheet == null) return;
            int records = 0;
            for (int rowIndex = 0; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                try
                {
                    var row = sheet.GetRow(rowIndex);
                    if (row == null)
                        continue;

                    var temp = row.GetCell(0).ToString();
                    if (!_startProcess && temp == _catalogDefinition.Rows[0].Name)
                        _startProcess = true;

                    if (_startProcess)
                    {
                        JTokenWriter writer = new JTokenWriter();
                        writer.WriteStartObject();
                        foreach (var t in _catalogDefinition.Rows)
                        {
                            ICell cell = row.GetCell(t.Index);
                            writer.WritePropertyName(t.PropertyName);
                            writer.WriteValue(cell.ToString());
                        }

                        writer.WriteEndObject();
                        //JObject o = (JObject)writer.Token;
                        records++;
                        Console.Write(writer.Token);
                        Console.WriteLine();
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
    }
}
