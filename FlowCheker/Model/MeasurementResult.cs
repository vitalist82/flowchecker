﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCheker.Model
{
    public class MeasurementResult
    {
        public DateTime Timestamp { get; set; }
        public MeasurementSettingsEntry Settings { get; set; }
        public List<dynamic> Values { get; set; }

        public MeasurementResult(MeasurementSettingsEntry settings, List<dynamic> values, DateTime timestamp)
        {
            Settings = settings;
            Values = values;
            Timestamp = timestamp;
        }
    }
}
