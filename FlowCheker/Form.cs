using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowCheker
{
    public partial class Form : System.Windows.Forms.Form, IForm
    {
        public event EventHandler StartEvent;

        public Form()
        {
            InitializeComponent();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartEvent?.Invoke(this, e);
        }
    }
}
