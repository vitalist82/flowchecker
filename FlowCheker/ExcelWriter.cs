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
    public class ExcelWriter : IResultWriter, IDisposable
    {
        private ExcelPackage excel;

        public ExcelWriter(string filePath)
        {
            CreateFileIfDoesntExist(filePath);
        }

        public void Dispose()
        {
            if (excel != null)
                excel.Dispose();
        }

        public void Write(MeasurementResult result)
        {
            string name = GetNormalizedName(result.Name);
            var ws = excel.Workbook.Worksheets.FirstOrDefault(worksheet => worksheet.Name == name);
            if (ws == null)
                ws = excel.Workbook.Worksheets.Add(name);
            int rowIndex = ws.Dimension != null ? ws.Dimension.End.Row : 0;
            ws.Cells[rowIndex + 1, 1].LoadFromCollection<string>(result.Values.Select(item => item as string));
            excel.Save();
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
