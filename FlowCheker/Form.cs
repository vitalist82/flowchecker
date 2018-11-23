using FlowCheker.Settings;
using System;

namespace FlowCheker
{
    public partial class Form : System.Windows.Forms.Form, IForm
    {
        private MeasurementSettings settings;

        public event EventHandler<SettingsEventArgs> SettingsUpdatedEvent;
        public event EventHandler StopEvent;
        public event EventHandler StartEvent;

        public MeasurementSettings Settings
        {
            get { return settings; }
            set
            {
                settings = value;
                PopulateForm();
            }
        }

        public Form()
        {
            InitializeComponent();
        }

        private void PopulateForm()
        {
            if (settings == null)
                return;

            listBoxCheckPoints.Items.Clear();
            settings?.Entries?.ForEach(item => listBoxCheckPoints.Items.Add(item.Name));
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartEvent?.Invoke(this, e);
        }
    }
}
