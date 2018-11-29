using FlowCheker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCheker.Interface
{
    public interface IResultWriter : IDisposable
    {
        void Write(MeasurementResult result);
    }
}
