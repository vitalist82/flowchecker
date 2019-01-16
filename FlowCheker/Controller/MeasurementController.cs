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
        private ResultWriterController<CsvWriter> writerController;
        private StatusModel statusModel;

        public MeasurementController(MeasurementSettings measurementSettings, StatusModel statusModel)
        {
            this.measurementSettings = measurementSettings;
            this.idsToTimers = new Dictionary<int, MeasurementTimer>();
            this.downloader = new Downloader();
            this.statusModel = statusModel;
        }

        public void Start()
        {
            Logger.Log(LogLevel.Info, "Periodical checks for all entries started.");
            if (isRunning)
            {
                Logger.Log(LogLevel.Info, "Checking in progress. Skipping.");
                return;
            }

            Logger.Log(LogLevel.Debug, "Starting ResultWriterController.");
            writerController = new ResultWriterController<CsvWriter>(new CsvWriter(), writerInterval, statusModel);
            writerController.Start();

            isRunning = true;
            statusModel.StatusMessage = "Checking in progress...";
            foreach(MeasurementSettingsEntry entry in measurementSettings.Entries)
            {
                Logger.Log(LogLevel.Debug, "Starting timer for " + entry.Name);
                var timer = !idsToTimers.ContainsKey(entry.Id) ?
                    CreateMeasurementTimer(entry) : idsToTimers[entry.Id];
                timer.Start();
            }
        }

        public void Stop()
        {
            Logger.Log(LogLevel.Info, "Stopping all checks.");
            foreach(int id in idsToTimers.Keys)
                idsToTimers[id]?.Stop();

            Logger.Log(LogLevel.Debug, "Stopping writer controller.");
            writerController?.Stop();
            isRunning = false;
            statusModel.StatusMessage = "Idle";
        }

        private Timer CreateMeasurementTimer(MeasurementSettingsEntry entry)
        {
            var timer = new MeasurementTimer(entry);
            timer.Interval = entry.UpdateInterval;
            timer.Elapsed += Timer_Elapsed;
            idsToTimers[entry.Id] = timer;
            return timer;
        }

        private async void CheckState(MeasurementSettingsEntry settingsEntry)
        {
            string statusMessage = "Checking state of '" + settingsEntry.Name + "'";
            Logger.Log(LogLevel.Info, statusMessage);
            statusModel.StatusMessage = statusMessage;

            try
            {
                List<dynamic>[] lines = await downloader.GetLastRows(settingsEntry.Url, settingsEntry.Selector);
                foreach (List<dynamic> line in lines)
                {
                    Logger.Log(LogLevel.Debug, "Adding line to writer controller.");
                    writerController.AddToQueue(new MeasurementResult(settingsEntry, line, DateTime.Now));
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, "CheckState failed with an exception: " + ex.Message);
            }
            finally
            {
                Logger.Log(LogLevel.Info, "CheckState of '" + settingsEntry.Name + "' finished.");
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            MeasurementSettingsEntry settingsEntry = ((MeasurementTimer)sender)?.MeasurementSettingsEntry;
            CheckState(settingsEntry);
        }
    }
}
