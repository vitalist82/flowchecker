using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCheker.Settings
{
    public class MeasurementSettingsEntry
    {
        public string Url { get; set; }
        public string Selector { get; set; }
        public string Name { get; set; }
        public int UpdateInterval { get; set; }

        public MeasurementSettingsEntry()
        {
        }

        public MeasurementSettingsEntry(string url, string selector, string name)
        {
            Url = url;
            Selector = selector;
            Name = name;
        }
    }
}
