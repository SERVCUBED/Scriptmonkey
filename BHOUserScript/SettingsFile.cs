using System;
using System.Collections.Generic;

namespace BHOUserScript
{
    /// <summary>
    /// Sets defaults for the settings file.
    /// </summary>
    public class SettingsFile
    {
        public bool AllowUserEdit = true;
        
        public bool Enabled = true;

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

        public bool InjectAPI = true;

        public bool InjectNotificationAPI = true;

        public bool CacheScripts = true;

        public bool LogScriptContentsOnRunError = false;

        public bool LogJScriptErrors = false;

        public bool UpdateDisabledScripts = false;

        public string[] BlacklistedUrls = new string[0];

        public int ReloadAfterPages = 5;

        public bool UseScriptmonkeyLink = true;

        public string ScriptmonkeyLinkUrl = "http://localhost:32888/";

        public int ScriptmonkeyLinkPollDelay = 3000;

        public AllowedScriptmonkeyLinkCommands AllowedScriptmonkeyLinkCommands = new AllowedScriptmonkeyLinkCommands();
    }
}
