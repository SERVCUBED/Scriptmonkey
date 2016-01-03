using System;
using System.Collections.Generic;

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

        public Version BhoCreatedVersion;

        public DateTime LastUpdateCheckDate;

        public bool CheckForUpdates = true;

        public bool RefreshOnSave = true;

        public bool RunOnPageRefresh = false;
    }
}
