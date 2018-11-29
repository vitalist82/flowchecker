using FlowCheker.Interface;
using FlowCheker.Model;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FlowCheker
{
    public partial class Form : System.Windows.Forms.Form, IForm
    {
        private MeasurementSettings settings;
        private BindingSource bindingSource;

        public event EventHandler<SettingsEventArgs> SettingsUpdatedEvent;
        public event EventHandler StopEvent;
        public event EventHandler StartEvent;
        public event EventHandler AddEntryEvent;
        public event EventHandler<RemoveEntryEventArgs> RemoveEntryEvent;

        public MeasurementSettings Settings
        {
            get { return settings; }
            set
            {
                settings = value;
                if (settings != null)
                    PopulateListBoxWithEntries();
            }
        }

        public Form()
        {
            InitializeComponent();
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK);
        }

        private void PopulateListBoxWithEntries()
        {
            if (bindingSource != null)
                bindingSource.ResetBindings(false);
        }

        #region Event Handlers

        private void Form_Load(object sender, EventArgs e)
        {
            if (settings == null)
                return;

            bindingSource = new BindingSource
            {
                DataSource = settings.Entries
            };

            listBoxEntries.DisplayMember = "Name";
            listBoxEntries.ValueMember = "Id";
            listBoxEntries.DataSource = bindingSource;

            tbName.DataBindings.Add("Text", bindingSource, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
            tbUrl.DataBindings.Add("Text", bindingSource, "Url", true, DataSourceUpdateMode.OnPropertyChanged);
            tbSelector.DataBindings.Add("Text", bindingSource, "Selector", true, DataSourceUpdateMode.OnPropertyChanged);
            tbInterval.DataBindings.Add("Text", bindingSource, "UpdateInterval", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartEvent?.Invoke(this, e);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopEvent?.Invoke(this, e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEntryEvent?.Invoke(this, e);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Do you really want to remove selected item?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Cancel)
                return;
            
            RemoveEntryEvent?.Invoke(this, new RemoveEntryEventArgs(settings.Entries[listBoxEntries.SelectedIndex].Id));
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsUpdatedEvent?.Invoke(this, new SettingsEventArgs(settings));
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: exit properly
            Application.Exit();
        }

        #endregion Event Handlers        
    }
}
