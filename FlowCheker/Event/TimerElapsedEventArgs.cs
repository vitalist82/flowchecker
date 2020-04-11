﻿using FlowCheker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCheker.Event
{
    public class TimerElapsedEventArgs : EventArgs
    {
        public MeasurementSettingsEntry MeasurementSettingsEntry { get; set; }

        public TimerElapsedEventArgs(MeasurementSettingsEntry measurementSettingsEntry)
        {
            MeasurementSettingsEntry = measurementSettingsEntry;
        }
    }
}
