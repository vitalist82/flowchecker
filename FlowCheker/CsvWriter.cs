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
        public void Write(MeasurementResult result)
        {
            using (FileStream fs = new FileStream(result.Settings.OutputFile, FileMode.OpenOrCreate))
            {
                long lastTimestamp = ReadLastTimestamp(fs);
                string currentContent = GetFileContent(fs);

                long resultTime = DateTime.Parse(result.Values[0]).ToFileTimeUtc();

                if (resultTime <= lastTimestamp)
                    return;

                using (StreamWriter sw = new StreamWriter(fs))
                {
                    string line = String.Join(";", result.Values);
                    Logger.Log(LogLevel.Info, line);
                    sw.WriteLine(line);
                    if (currentContent != String.Empty)
                        sw.WriteLine(currentContent);
                }

                lastTimestamp = resultTime;
            }
        }

        private string GetFileContent(FileStream fs)
        {
            if (fs.Length == 0)
                return String.Empty;

            StreamReader sr = new StreamReader(fs);
            return sr.ReadToEnd();
        }

        private long ReadLastTimestamp(FileStream stream)
        {
            if (stream.Length == 0)
                return 0;

            try
            {
                string line = null;
                StreamReader sr = new StreamReader(stream);
                while ((line = sr.ReadLine()) != null) ;
                return line != null ? DateTime.Parse(line.Split(';')[0]).ToFileTimeUtc() : 0;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
    }
}
