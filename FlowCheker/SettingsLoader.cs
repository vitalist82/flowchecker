﻿using FlowCheker.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCheker
{
    public class SettingsLoader<T> : ISettingsLoader<T> where T : new()
    {
        public T Load(string fileName)
        {
            if (!File.Exists(fileName))
                return new T();

            string json = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void Save(T settings, string fileName)
        {
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(fileName, json);
        }
    }
}
