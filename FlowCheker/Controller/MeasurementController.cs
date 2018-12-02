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
        private static int writerInterval = 3000;

        private bool isRunning;
        private Dictionary<int, MeasurementTimer> idsToTimers;
        private MeasurementSettings measurementSettings;
        private Downloader downloader;
        private ResultWriterController<ExcelWriter> writerController;

        public MeasurementController(MeasurementSettings measurementSettings)
        {
            this.measurementSettings = measurementSettings;
            this.idsToTimers = new Dictionary<int, MeasurementTimer>();
            this.downloader = new Downloader();
        }

        public void Start()
        {
            if (isRunning)
                return;

            writerController = new ResultWriterController<ExcelWriter>(new ExcelWriter(measurementSettings.OutputFile), writerInterval);
            writerController.Start();

            isRunning = true;
            foreach(MeasurementSettingsEntry entry in measurementSettings.Entries)
            {
                if (!idsToTimers.ContainsKey(entry.Id))
                    CreateMeasurementTimer(entry);
                idsToTimers[entry.Id]?.Start();
            }
        }

        public void Stop()
        {
            foreach(int id in idsToTimers.Keys)
                idsToTimers[id]?.Stop();

            writerController?.Stop();
            isRunning = false;
        }

        private void CreateMeasurementTimer(MeasurementSettingsEntry entry)
        {
            var timer = new MeasurementTimer(entry);
            timer.Interval = entry.UpdateInterval;
            timer.Elapsed += Timer_Elapsed;
            idsToTimers[entry.Id] = timer;
        }

        private async void CheckState(MeasurementSettingsEntry settingsEntry)
        {
            try
            {
                Console.WriteLine(settingsEntry.Name);
                Console.WriteLine(settingsEntry.UpdateInterval);
                string[] lines = await downloader.GetLastRows(settingsEntry.Url, settingsEntry.Selector);
                foreach (string line in lines)
                    writerController.AddToQueue(new MeasurementResult(settingsEntry.Name, line.Split(';').ToList<dynamic>(), DateTime.Now));
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
