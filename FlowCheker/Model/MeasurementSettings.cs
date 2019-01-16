using System.Collections.Generic;

namespace FlowCheker.Model
{
    public class MeasurementSettings
    {
        public string SettingsFile { get; set; }
        public List<MeasurementSettingsEntry> Entries { get; set; }

        public MeasurementSettings()
        {}
    }
}
