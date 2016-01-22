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

        public string MenuCommandCSS =
            "position: fixed; bottom: 0; right: 0; font-size: 12px; width: 100px; padding: 5px; border-radius: 3px; background: #333; z-index: 2000000000;";

        public List<Script> Scripts = new List<Script>();

        public Version BhoCreatedVersion;

        public DateTime LastUpdateCheckDate;

        public bool CheckForUpdates = true;

        public bool RefreshOnSave = true;

        public bool RunOnPageRefresh = false;

        public bool AutoDownloadScripts = true;

        public bool UsePublicAPI = false;
    }
}
