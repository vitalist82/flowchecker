using FlowCheker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCheker.Event
{
    public class SettingsEventArgs : EventArgs
    {
        public MeasurementSettings Settings { get; }

        public SettingsEventArgs(MeasurementSettings settings)
        {
            Settings = settings;
        }
    }
}
