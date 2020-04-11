using FlowCheker.Event;
using FlowCheker.Interface;
using FlowCheker.Model;
using System;
using System.Timers;

namespace FlowCheker
{
    public delegate void TimerElapsedEventHandler(object sender, TimerElapsedEventArgs e);

    public class MeasurementTimer : IMeasurementTimer
    {
        public MeasurementSettingsEntry MeasurementSettingsEntry { get { return measurementSettingsEntry; } }
        public event EventHandler<TimerElapsedEventArgs> Elapsed;
        
        private MeasurementSettingsEntry measurementSettingsEntry;
        private Timer timer;

        public MeasurementTimer(MeasurementSettingsEntry measurementSettingsEntry) : base()
        {
            this.measurementSettingsEntry = measurementSettingsEntry;
        }

        public void Start()
        {
            if (measurementSettingsEntry.IsFrequencyEnabled)
                StartFrequencyBasedTimer();
            StartIntervalBasedTimer();
        }

        public void Stop()
        {
            if (timer != null)
                timer.Stop();
        }

        private void StartFrequencyBasedTimer()
        {
            TimeSpan delay = measurementSettingsEntry.NextMeasurementTime.TimeOfDay - DateTime.Now.TimeOfDay;
            if (delay > )
        }

        private void StartIntervalBasedTimer()
        {
            timer = new Timer(measurementSettingsEntry.UpdateInterval);
            timer.Elapsed += IntervalBasedTimerElapsed;
            timer.Start();
        }

        private void IntervalBasedTimerElapsed(object sender, EventArgs e)
        {
            if (Elapsed != null)
                Elapsed(this, new TimerElapsedEventArgs(measurementSettingsEntry));
        }
    }
}
