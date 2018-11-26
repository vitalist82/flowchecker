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
        private MeasurementSettingsEntry measurementSettingsEntry;

        public MeasurementSettingsEntry MeasurementSettingsEntry {  get { return measurementSettingsEntry;  } }

        public MeasurementTimer(MeasurementSettingsEntry measurementSettingsEntry) : base()
        {
            this.measurementSettingsEntry = measurementSettingsEntry;
        }
    }
}
