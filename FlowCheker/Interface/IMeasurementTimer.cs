using FlowCheker.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCheker.Interface
{
    interface IMeasurementTimer
    {
        void Start();
        void Stop();
        event EventHandler<TimerElapsedEventArgs> Elapsed;
    }
}
