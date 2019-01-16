using FlowCheker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FlowCheker
{
    public class MeasurementTimer : Timer
    {
        public MeasurementSettingsEntry MeasurementSettingsEntry { get { return measurementSettingsEntry; } }

        private MeasurementSettingsEntry measurementSettingsEntry;

        public MeasurementTimer(MeasurementSettingsEntry measurementSettingsEntry) : base()
        {
            this.measurementSettingsEntry = measurementSettingsEntry;
        }
    }
}
