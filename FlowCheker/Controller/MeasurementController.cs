using FlowChecker;
using FlowCheker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FlowCheker.Controller
{
    public class MeasurementController
    {
        private Dictionary<int, MeasurementTimer> idsToTimers;
        private bool isRunning;
        private MeasurementSettings measurementSettings;

        public MeasurementController(MeasurementSettings measurementSettings)
        {
            this.measurementSettings = measurementSettings;
            this.idsToTimers = new Dictionary<int, MeasurementTimer>();
        }

        public void Start()
        {
            isRunning = true;
            foreach(MeasurementSettingsEntry entry in measurementSettings.Entries)
            {
                MeasurementTimer timer = null;
                if (!idsToTimers.ContainsKey(entry.Id))
                {
                    timer = new MeasurementTimer(entry);
                    timer.Elapsed += Timer_Elapsed;
                    idsToTimers[entry.Id] = timer;
                }
                idsToTimers[entry.Id].Start();
            }
        }

        public void Stop()
        {
            foreach(int id in idsToTimers.Keys)
                idsToTimers[id].Stop();
            isRunning = false;
        }

        private async void CheckState(MeasurementSettingsEntry settingsEntry)
        {
            try
            {
                Console.WriteLine(settingsEntry.Name);
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            MeasurementSettingsEntry settingsEntry = ((MeasurementTimer)sender)?.MeasurementSettingsEntry;
            CheckState(settingsEntry);
        }
    }
}
