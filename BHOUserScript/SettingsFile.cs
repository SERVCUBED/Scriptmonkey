﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHOUserScript
{
    /// <summary>
    /// Sets defaults for the settings file.
    /// </summary>
    public class SettingsFile
    {
        public bool Enabled = true;

        public string EditorPath = "notepad.exe";

        public List<Script> Scripts = new List<Script>();

        public Version BHOCreatedVersion;
    }
}
