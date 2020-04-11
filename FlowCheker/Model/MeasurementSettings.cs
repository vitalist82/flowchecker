using System.Collections.Generic;

namespace FlowCheker.Model
{
    public class MeasurementSettings
    {
        private List<MeasurementSettingsEntry> entries;

        public List<MeasurementSettingsEntry> Entries
        {
            get
            {
                return this.entries;
            }
            set
            {
                value.Sort();
                this.entries = value;
            }
        }

        public MeasurementSettings()
        {}
    }
}
