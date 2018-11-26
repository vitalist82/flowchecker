using FlowCheker.Model;
using System;

namespace FlowCheker.Interface
{
    interface IForm
    {
        MeasurementSettings Settings { get; set; }
        event EventHandler StartEvent;
        event EventHandler StopEvent;
        event EventHandler AddEntryEvent;
        event EventHandler<RemoveEntryEventArgs> RemoveEntryEvent;
        event EventHandler<SettingsEventArgs> SettingsUpdatedEvent;
    }
}
