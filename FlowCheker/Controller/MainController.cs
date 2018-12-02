﻿using FlowCheker.Interface;
using FlowCheker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlowCheker.Controller
{
    // TODO: what is responsibility of this class? should it be SettingsController? should it be split to multiple classes?
    public class MainController
    {
        private const string settingsFileName = "settings.json";
        private const string outputFilePath = "output.xlsx";

        private IForm form;
        private MeasurementSettings measurementSettings;
        private MeasurementController measurementController;

        public MainController(IForm form)
        {
            this.form = form;
            Init();
        }

        public void Init()
        {
            LoadSettings();
            BindEvents();
            measurementController = new MeasurementController(measurementSettings);
            form.Settings = measurementSettings;
        }

        private void BindEvents()
        {
            form.StartEvent += Form_StartEvent;
            form.StopEvent += Form_StopEvent;
            form.SettingsUpdatedEvent += Form_SettingsUpdatedEvent;
            form.AddEntryEvent += Form_AddEntryEvent;
            form.RemoveEntryEvent += Form_RemoveEntryEvent;
        }

        private int GetNewId()
        {
            List<MeasurementSettingsEntry> entries = measurementSettings.Entries;
            int[] indexes = entries.Select(entry => entry.Id).ToArray();
            Array.Sort(indexes);
            int i = 0;
            for (; i < indexes.Length - 1; i++)
            {
                if (i == indexes.Length - 1
                    || indexes[i + 1] - indexes[i] > 1)
                    return indexes[i] + 1;
            }
            return indexes[i] + 1;
        }

        private void LoadSettings()
        {
            var loader = new SettingsLoader<MeasurementSettings>();
            measurementSettings = loader.Load(settingsFileName);
            if (measurementSettings.OutputFile == null)
                measurementSettings.OutputFile = outputFilePath;
        }

        private void SaveSettings()
        {
            var loader = new SettingsLoader<MeasurementSettings>();
            loader.Save(measurementSettings, settingsFileName);
        }

        private void Form_AddEntryEvent(object sender, EventArgs e)
        {
            measurementSettings.Entries.Add(new MeasurementSettingsEntry { Id = GetNewId(), Name = "Unknown", Selector = String.Empty, UpdateInterval = 0, Url = String.Empty });
            form.Settings = measurementSettings;
        }

        private void Form_RemoveEntryEvent(object sender, RemoveEntryEventArgs e)
        {
            measurementSettings.Entries.RemoveAll(entry => entry.Id == e.Id);
            form.Settings = measurementSettings;
        }

        private void Form_SettingsUpdatedEvent(object sender, SettingsEventArgs e)
        {
            measurementSettings = e.Settings;
            SaveSettings();
        }

        private void Form_StopEvent(object sender, EventArgs e)
        {
            measurementController.Stop();
        }

        private void Form_StartEvent(object sender, EventArgs e)
        {
            measurementController.Start();
        }
    }
}
