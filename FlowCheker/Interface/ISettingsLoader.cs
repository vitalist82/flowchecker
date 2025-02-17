﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowCheker.Interface
{
    interface ISettingsLoader<T>
    {
        T Load(string fileName);
        void Save(T settings, string fileName);
    }
}
