using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCheker.Event
{
    public class StatusMessageEventArgs : EventArgs
    {
        public string StatusMessage { get; set; }

        public StatusMessageEventArgs(string message)
        {
            StatusMessage = message;
        }
    }
}
