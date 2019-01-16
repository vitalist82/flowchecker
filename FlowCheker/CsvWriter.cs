using FlowCheker.Interface;
using FlowCheker.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCheker
{
    public class CsvWriter : IResultWriter
    {
        public CsvWriter()
        {
        }

        public void Write(MeasurementResult result)
        {
            long lastTimestamp = ReadLastTimestamp(result.Settings.OutputFile);
            long resultTime = DateTime.Parse(result.Values[0]).ToFileTimeUtc();
            if (resultTime <= lastTimestamp)
                return;

            string currentContent = GetCurrentContent(result.Settings.OutputFile);
            using (FileStream fs = new FileStream(result.Settings.OutputFile, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    string line = String.Join(";", result.Values);
                    Logger.Log(LogLevel.Info, line);
                    sw.WriteLine(line);
                    if (currentContent != String.Empty)
                        sw.WriteLine(currentContent);
                }
            }
        }

        private string GetCurrentContent(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath);
            }
            catch
            {
                return String.Empty;
            }
        }

        private long ReadLastTimestamp(string filePath)
        {
            if (filePath == null || !File.Exists(filePath))
                return 0;

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string line = sr.ReadLine();
                        return line != null ? DateTime.Parse(line.Split(';')[0]).ToFileTimeUtc() : 0;
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Log(LogLevel.Error, "ReadLastTimestamp failed with an exception: " + ex.Message);
                return 0;
            }
        }
    }
}
