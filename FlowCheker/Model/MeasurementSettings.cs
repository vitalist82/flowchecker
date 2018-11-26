using System.Collections.Generic;

namespace FlowCheker.Model
{
    public class MeasurementSettings
    {
        public const string SettingsFileName = "settings.json";

        public string OutputFile { get; set; }
        public string SettingsFile { get; set; }
        public List<MeasurementSettingsEntry> Entries { get; set; }

        public MeasurementSettings()
        {}
    }
}
