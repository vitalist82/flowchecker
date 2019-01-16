using FlowCheker.Model;
using System;

namespace FlowCheker.Interface
{
    public interface IForm
    {
        MeasurementSettings Settings { get; set; }
        void AppendMessage(string message);
        event EventHandler StartEvent;
        event EventHandler StopEvent;
        event EventHandler AddEntryEvent;
        event EventHandler<RemoveEntryEventArgs> RemoveEntryEvent;
        event EventHandler<SettingsEventArgs> SettingsUpdatedEvent;
    }
}
