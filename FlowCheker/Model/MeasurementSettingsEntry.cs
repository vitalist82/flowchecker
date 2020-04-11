using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCheker.Model
{
    public class MeasurementSettingsEntry : IComparable<MeasurementSettingsEntry>
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Selector { get; set; }
        public string Name { get; set; }
        public bool IsFrequencyEnabled { get; set; }
        public int UpdateInterval { get; set; }
        public DateTime NextMeasurementTime { get; set; }
        public int MeasurementHoursFrequency { get; set; }
        public string OutputFile { get; set; }

        public MeasurementSettingsEntry()
        {
        }

        public MeasurementSettingsEntry(string url, string selector, string name, int updateInterval, bool isFrequencyEnabled,
            DateTime nextMeasurementTime, int hoursFrequency, string outputFile)
        {
            Url = url;
            Selector = selector;
            Name = name;
            UpdateInterval = updateInterval;
            IsFrequencyEnabled = IsFrequencyEnabled;
            NextMeasurementTime = nextMeasurementTime;
            MeasurementHoursFrequency = hoursFrequency;
            OutputFile = outputFile;
        }

        public int CompareTo(MeasurementSettingsEntry other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
