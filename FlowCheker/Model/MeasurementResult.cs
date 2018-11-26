using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCheker.Model
{
    public class MeasurementResult
    {
        public DateTime Timestamp { get; set; }
        public string Name { get; set; }
        public List<dynamic> Values { get; set; }

        public MeasurementResult(string name, List<dynamic> values, DateTime timestamp)
        {
            Name = name;
            Values = values;
            Timestamp = timestamp;
        }
    }
}
