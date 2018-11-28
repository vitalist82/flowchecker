using FlowChecker;
using FlowCheker.Interface;
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
        private Downloader downloader;
        private ResultWriterController<ExcelWriter> writer;

        public MeasurementController(MeasurementSettings measurementSettings)
        {
            this.measurementSettings = measurementSettings;
            this.idsToTimers = new Dictionary<int, MeasurementTimer>();
            this.downloader = new Downloader();
            this.writer = new ResultWriterController<ExcelWriter>(new ExcelWriter("C:\\Work\\temp\\output.xlsx"), 3000);
            this.writer.Start();
        }

        public void Start()
        {
            if (isRunning)
                return;

            isRunning = true;
            foreach(MeasurementSettingsEntry entry in measurementSettings.Entries)
            {
                MeasurementTimer timer = null;
                if (!idsToTimers.ContainsKey(entry.Id))
                {
                    timer = new MeasurementTimer(entry);
                    timer.Interval = entry.UpdateInterval;
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
            this.writer.Stop();
            isRunning = false;
        }

        private async void CheckState(MeasurementSettingsEntry settingsEntry)
        {
            try
            {
                Console.WriteLine(settingsEntry.Name);
                string[] lines = await downloader.GetLastRows(settingsEntry.Url, settingsEntry.Selector);
                foreach (string line in lines)
                    writer.AddToQueue(new MeasurementResult(settingsEntry.Name, line.Split(';').ToList<dynamic>(), DateTime.Now));
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
