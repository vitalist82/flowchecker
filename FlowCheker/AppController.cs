using FlowCheker.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCheker
{
    class AppController
    {
        IForm form;
        MeasurementSettings appSettings;

        public AppController(IForm form)
        {
            this.form = form;
            Init();
        }

        public void Init()
        {
            LoadSettings();
            form.StartEvent += Form_StartEvent;
            form.StopEvent += Form_StopEvent;
            form.SettingsUpdatedEvent += Form_SettingsUpdatedEvent;
            form.Settings = appSettings;
        }

        private void Form_SettingsUpdatedEvent(object sender, SettingsEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Form_StopEvent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LoadSettings()
        {
            var loader = new SettingsLoader<MeasurementSettings>();
            appSettings = loader.Load(MeasurementSettings.SettingsFileName);

            appSettings = new MeasurementSettings()
            {
                Entries = new List<MeasurementSettingsEntry>()
                {
                    { new MeasurementSettingsEntry() { Name = "assadf", Selector = "/tr/td/d", UpdateInterval = 5000, Url = "http://asd.ertw.df" } },
                    { new MeasurementSettingsEntry() { Name = "azxccv", Selector = "/et/asd/{0}", UpdateInterval = 3000, Url = "https://aasdf.tryert.vc"} }
                }
            };
        }

        private void Form_StartEvent(object sender, EventArgs e)
        {
            
        }
    }
}
