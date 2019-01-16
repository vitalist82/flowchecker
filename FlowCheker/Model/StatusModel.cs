using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCheker.Model
{
    public class StatusModel
    {
        public event EventHandler<StatusMessageEventArgs> StatusMessageChanged;
        private string statusMessage = "Idle";

        public string StatusMessage
        {
            get
            {
                return statusMessage;
            }

            set
            {
                statusMessage = value;
                StatusMessageChanged.Invoke(this, new StatusMessageEventArgs(statusMessage));
            }
        }
    }
}
