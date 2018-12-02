using FlowCheker.Interface;
using FlowCheker.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCheker
{
    public class ExcelWriter : IResultWriter
    {
        private ExcelPackage excel;
        private Dictionary<string, DateTime> lastTimestamps;

        public ExcelWriter(string filePath)
        {
            CreateFileIfDoesntExist(filePath);
            ReadLastTimestamps();
        }

        public void Dispose()
        {
            if (excel != null)
                excel.Dispose();
        }

        public void Write(MeasurementResult result)
        {
            string name = GetNormalizedName(result.Name);
            DateTime resultTime = DateTime.Parse(result.Values[0]);
            if (lastTimestamps.ContainsKey(name) && lastTimestamps[name] >= resultTime)
                return;

            var ws = excel.Workbook.Worksheets.FirstOrDefault(worksheet => worksheet.Name == name);
            if (ws == null)
                ws = excel.Workbook.Worksheets.Add(name);
            int rowIndex = ws.Dimension != null ? ws.Dimension.End.Row + 1 : 1;

            for (int i = 1; i <= result.Values.Count; i++)
                ws.Cells[rowIndex, i].Value = result.Values[i-1];
            excel.Save();
            lastTimestamps[name] = resultTime;
        }

        private void ReadLastTimestamps()
        {
            lastTimestamps = new Dictionary<string, DateTime>();
            foreach(var ws in excel.Workbook.Worksheets)
            {
                if (ws.Dimension == null)
                    continue;

                int lastRowIndex = ws.Dimension.End.Row;
                DateTime timestamp = DateTime.Parse((string) ws.Cells[ws.Dimension.End.Row, 1].Value);
                lastTimestamps.Add(ws.Name, timestamp);
            }
        }

        private string GetNormalizedName(string name)
        {
            return name.Trim();
        }

        private void CreateFileIfDoesntExist(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            excel = new ExcelPackage(fi);
        }
    }
}
