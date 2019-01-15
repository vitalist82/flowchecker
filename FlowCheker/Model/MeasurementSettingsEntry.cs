using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCheker.Model
{
    public class MeasurementSettingsEntry
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Selector { get; set; }
        public string Name { get; set; }
        public int UpdateInterval { get; set; }
        public string OutputFile { get; set; }

        public MeasurementSettingsEntry()
        {
        }

        public MeasurementSettingsEntry(string url, string selector, string name, int updateInterval, string outputFile)
        {
            Url = url;
            Selector = selector;
            Name = name;
            UpdateInterval = updateInterval;
            OutputFile = outputFile;
        }
    }
}
