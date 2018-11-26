using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCheker
{
    public class RemoveEntryEventArgs : EventArgs
    {
        public int Id { get; }

        public RemoveEntryEventArgs(int id)
        {
            Id = id;
        }
    }
}
