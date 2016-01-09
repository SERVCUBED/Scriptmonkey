using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using mshtml;
using Microsoft.Win32;
using Newtonsoft.Json;
using SHDocVw;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices.Expando;
using System.Security.Permissions;
using System.Collections.Generic;

/**
 *       ____        _     __                 __           
 *      / __________(____ / /___ _ ___  ___  / /_____ __ __
 *     _\ \/ __/ __/ / _ / __/  ' / _ \/ _ \/  '_/ -_/ // /
 *    /___/\__/_/ /_/ .__\__/_/_/_\___/_//_/_/\_\\__/\_, / 
 *                 /_/                              / / /  
 *    Userscript manager BHO For Internet Explorer /___/
 * 
 *                        * * *
 * 
 * Released open-source under GPL.
 * 
 * Original Author: Ben Blain (mail[at]servc.eu)
 * 
 * See the todo list on GitHub if you would like to contribute.
 *       
 */

// Original name was BHOUserScript (later changed to Scriptmonkey, which is more user-friendly)
namespace BHOUserScript
{
    /// <summary>
    /// Scriptmonky BHO (Browser Helper Object) userscript manager for Internet Explorer.
    /// </summary>
    [ComVisible(true),
    ClassInterface(ClassInterfaceType.None),
    Guid("7f724466-7c51-4b14-80a1-7b0568b4226c"),
    ProgId("Scriptmonkey"),
    ComDefaultInterface(typeof(IExtension))]
    public class Scriptmonkey : IObjectWithSite, IOleCommandTarget, IExtension
    {
        #region Declarations
        IWebBrowser2 _browser;
        private object _site;
        private bool _installChecked;
        // Contents of Wrapper.js, minified and split to allow scriptIndex to be set easily
        private readonly string wrapperJS_before = "function GM_deleteValue(e){window.Scriptmonkey.deleteScriptValue(e,scriptIndex)}" +
            "function GM_getValue(e,t){return window.Scriptmonkey.getScriptValue(e,t,scriptIndex)}function GM_listValues(){" +
            "return window.Scriptmonkey.getScriptValuesList(scriptIndex)}function GM_setValue(e,t){window.Scriptmonkey.setScriptValue(e,t,scriptIndex)}" +
        "function GM_addStyle(e){css=document.createElement(\"style\"),css.type=\"text/css\",css.innerHTML=e,document.body.appendChild(css)}" +
        "function GM_openInTab(e){window.open(e)}function GM_log(e){console.log(\"Scriptmonkey: \"+e)}function GM_setClipboard(e){" +
        "window.Scriptmonkey.setClipboard(e)}function GM_getResourceText(e){window.Scriptmonkey.getScriptResourceText(e,scriptIndex)}" +
        "function GM_getResourceURL(e){window.Scriptmonkey.getScriptResourceUrl(e,scriptIndex)}scriptIndex=";
        private readonly string wrapperJS_after = ",unsafeWindow=window;";
        private object currentURL;
        private bool refresh = false;
        private bool normalLoad = true;

        public static readonly string InstallPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
            + Path.DirectorySeparatorChar + ".Scriptmonkey" + Path.DirectorySeparatorChar;
        public static readonly string ScriptPath = InstallPath + "scripts" + Path.DirectorySeparatorChar;
        public static readonly string ResourcePath = InstallPath + "resources" + Path.DirectorySeparatorChar;
        public static readonly string SettingsFile = InstallPath + "settings.json";
        public static readonly string InstalledFile = InstallPath + "installed";

        private Db _prefs;
        #endregion

        #region Is installed?
        /// <summary>
        /// Checks for a valid installation. If Scriptmonkey is not installed, it creates the config files and directories.
        /// </summary>
        public void CheckInstall()
        {
            if(!File.Exists(InstalledFile))
            {
                StreamWriter jsonDb = new StreamWriter(SettingsFile);
                try
                {
                    MessageBox.Show(Resources.FirstTimeSetup, Resources.Title);
                    Directory.CreateDirectory(InstallPath);
                    Directory.CreateDirectory(ScriptPath);
                    Directory.CreateDirectory(ResourcePath);
                    File.Create(InstalledFile).Dispose();

                    SettingsFile s = new SettingsFile();
                    s.BhoCreatedVersion = CurrentVersion();
                    s.LastUpdateCheckDate = DateTime.Now;

                    jsonDb.Write(JsonConvert.SerializeObject(s)); // Write blank json settings file
                    MessageBox.Show(Resources.FirstTimeSetupDone, Resources.Title);
                }
                catch (Exception ex)
                {
                    Log(ex, "Installer");
                }
                jsonDb.Close();
            }

            _prefs.LoadData();

            // Has BHO been updated? Settings file needs to be updated
            _prefs.CheckUpdateStatus();

            _installChecked = true;
        }
        #endregion

        #region Main Function
        /// <summary>
        /// This is the main function which is run on page load. It injects userscripts into the page.
        /// </summary>
        /// <param name="pDisp">Browser object (for page currently displayed)</param>
        /// <param name="url">Current URL</param>
        void Run(object pDisp, ref object url) // OnDocumentComplete handler
        {
            currentURL = url;

            if ((_browser.Document as IHTMLDocument2) == null) // Prevents run from Windows Explorer
                return;

            if (_browser.Document == null) // No point running if no document is being displayed
                return;

            try
            {

                if (pDisp != this._site)
                    return;

                if (_prefs == null)
                    _prefs = new Db(this);

                refresh = true;
                if (_prefs.Settings.RunOnPageRefresh)
                    normalLoad = false;

                if (_prefs.AllScripts.Count > 10) // Lots of scripts?
                    _prefs.ReloadDataAsync(); // Reload asynchronously to prevent browser becoming unresponsive
                else
                    _prefs.ReloadData();

                if (_prefs.Settings.Enabled)
                {
                    var document2 = _browser.Document as HTMLDocument;
                    //var document3 = browser.Document as IHTMLDocument3;

                    var window = document2.parentWindow;

                    SetupWindow(window);

                    if (document2.url.Contains("https://servc.eu/p/scriptmonkey/"))
                        try
                        {
                            // Tell webpage Scriptmonkey is installed
                            var v = Scriptmonkey.CurrentVersion();
                            window.execScript(String.Format("ld_Scriptmonkey_Installed({0},{1},{2},{3});", v.Major, v.Minor, v.Revision, _prefs.AllScripts.Count));
                        }
                        catch (Exception ex)
                        {
                            Log(ex, "\r\nUnable to inject JavaScript into webpage");
                        }

                    if (document2.url == "https://servc.eu/p/scriptmonkey/options.html")
                        ShowOptions();
                    // Check if user wants to install script
                    else if (document2.url.Contains(".user.js"))
                    {
                        AddScriptUrlFrm form = new AddScriptUrlFrm();
                        if (form.ShowDialog() == DialogResult.OK) // Automatically
                        {
                            try
                            {
                                WebClient webClient = new WebClient();
                                webClient.DownloadFile(document2.url, ScriptPath
                                    + document2.url.Substring(document2.url.LastIndexOf('/') + 1));
                                Script s = ParseScriptMetadata.Parse(document2.url.Substring(document2.url.LastIndexOf('/')));
                                s.Path = document2.url.Substring(document2.url.LastIndexOf('/') + 1);
                                _prefs.AddScript(s);
                            }
                            catch (Exception ex)
                            {
                                Log(ex, Resources.AutomaticAddFailError + ":AutoAdd");
                            }
                        }
                        form.Dispose();
                    }

                    for (int i = 0; i < _prefs.AllScripts.Count; i++)
                    {
                        if (_prefs[i].Enabled)
                        {
                            bool shouldRun = true;
                            if (_prefs[i].Include.Length > 0)
                            {
                                shouldRun = false; // Default to false
                                if (Regex.IsMatch(url.ToString(), WildcardToRegex(_prefs[i].Include)))
                                {
                                    shouldRun = true;
                                }
                            }

                            if (_prefs[i].Exclude.Length > 0)
                            {
                                if (Regex.IsMatch(url.ToString(), WildcardToRegex(_prefs[i].Exclude)))
                                {
                                    shouldRun = false;
                                }
                            }

                            if (shouldRun)
                            {
                                StreamReader str = new StreamReader(ScriptPath + _prefs[i].Path);
                                try
                                {
                                    string c = str.ReadToEnd();
                                    window.execScript("(function(){" + wrapperJS_before + i + wrapperJS_after + c + "})();");
                                }
                                catch (Exception ex) {
                                    window.execScript("console.log(\"Scriptmonkey: Unable to load script: " + _prefs[i].Name + ". Error: " + ex.Message.Replace("\"", "\\\"") + "\");");
                                    Log(ex, "\r\nAt script: " + _prefs[i].Name);
                                }
                                str.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log(ex, "\r\nAt: main");
                CheckInstall(); // Error may be caused by invalid installation. Verify files haven't been deleted.
            }
        }

        /// <summary>
        /// Exposes the IExtension interface to the browser window.
        /// </summary>
        /// <param name="window"></param>
        public void SetupWindow(dynamic window)
        {
            try
            {
                IExpando exp = (IExpando)window;
                PropertyInfo info = exp.AddProperty("Scriptmonkey");
                info.SetValue(exp, this);
            }
            catch (Exception ex)
            {
                Log(ex, "SetupWindow");
            }
        }

        /// <summary>
        /// Gets the version of the current assembly.
        /// </summary>
        /// <returns>Assembly version</returns>
        public static Version CurrentVersion()
        {
            var thisApp = Assembly.GetExecutingAssembly();
            AssemblyName name = new AssemblyName(thisApp.FullName);
            return name.Version;
        }

        /// <summary>
        /// Gets the current path of the assembly.
        /// </summary>
        /// <returns>Assembly Path</returns>
        public static string AssemblyPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "Scriptmonkey.dll";
        }

        /// <summary>
        /// Convert a string array of @match values into Regex.
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns>Regex pattern</returns>
        public static string WildcardToRegex(string[] pattern)
        {
            string _out = String.Empty;
            for (int i = 0; i < pattern.Length; i++)
            {
                // Allow regex in include/match (http://wiki.greasespot.net/Include_and_exclude_rules#Regular_Expressions)
                if (pattern[i].Length > 3 && pattern[i].StartsWith("/") && pattern[i].EndsWith("/"))
                    _out += pattern[i].Substring(1, pattern[i].Length - 1); // Remove leading and trailing '/'
                else
                {
                    _out += Regex.Escape(pattern[i]).
                        Replace("\\*", ".*").
                        Replace("\\?", ".");
                }
                if (i < pattern.Length - 1) // If not the last item
                    _out += "|";
            }
            return _out;
        }

        /// <summary>
        /// Writes an exception to the log file.
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="extraInfo">Any extra info (debug notes, vars)</param>
        public static void Log(Exception ex, string extraInfo = null)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(Scriptmonkey.InstallPath + "log.txt"))
                {
                    if (ex != null)
                    {
                        writer.WriteLine(ex.Message);
                        writer.WriteLine(ex.StackTrace);
                    }
                    if (extraInfo != null) 
                        writer.WriteLine(extraInfo);
                    writer.WriteLine("------------ " + DateTime.Now);
                    writer.Close();
                }
            }
            catch (Exception) { }
            //MessageBox.Show(ex.Message + Environment.NewLine + "Stack: " + Environment.NewLine + ex.StackTrace + Environment.NewLine + "Souce: " + Environment.NewLine + ex.Source + ": Main" + ((extraInfo != null)? Environment.NewLine + extraInfo : ""), Resources.Title);
        }

        /// <summary>
        /// Check for updates from a URL, then prompt the user to install them.
        /// </summary>
        private void CheckUpdate()
        {
            if (_prefs.Settings.CheckForUpdates && _prefs.Settings.LastUpdateCheckDate < DateTime.Now - TimeSpan.FromDays(3))
            {
                WebResponse wr = null;
                Stream resStream = null;
                try
                {
                    HttpWebRequest wc = (HttpWebRequest)WebRequest.Create(new Uri("https://servc.eu/p/scriptmonkey/version.php"));
                    StringBuilder sb = new StringBuilder();
                    byte[] buf = new byte[8192];
                    wr = wc.GetResponse();
                    resStream = wr.GetResponseStream();
                    
                    string tempString = null;
                    int count = 0;
                    do
                    {
                        count = resStream.Read(buf, 0, buf.Length);
                        if (count != 0)
                        {
                            tempString = Encoding.ASCII.GetString(buf, 0, count);
                            sb.Append(tempString);
                        }
                    }
                    while (count > 0);

                    UpdateResponse response = JsonConvert.DeserializeObject<UpdateResponse>(sb.ToString());

                    if (response.Success && response.LatestVersion > CurrentVersion())
                    {
                        // Got valid response from server and there is an update. Now to ask user to update.
                        UpdateBHOFrm form = new UpdateBHOFrm();
                        form.currentVersionTxt.Text = CurrentVersion().ToString();
                        form.newVersionTxt.Text = response.LatestVersion.ToString();
                        form.textBox1.Text = response.Changes;
                        form.ShowDialog();

                        if (form.Response != UpdateBHOFrm.UpdateBHOFrmResponse.NextTime)
                        {
                            _prefs.Settings.LastUpdateCheckDate = DateTime.Now;
                            _prefs.Save();
                            _prefs.ReloadData();
                        }
                        if (form.Response == UpdateBHOFrm.UpdateBHOFrmResponse.Now)
                        {
                            new Process
                            {
                                StartInfo =
                                {
                                    FileName = "iexplore.exe",
                                    Arguments = "https://servc.eu/p/scriptmonkey/update.html"
                                }
                            }.Start();

                        }
                    }
                    else
                    {
                        _prefs.Settings.LastUpdateCheckDate = DateTime.Now;
                        _prefs.Save();
                        _prefs.ReloadData();
                    }
                }
                catch (Exception) { }

                if (wr != null)
                    wr.Close();
                if (resStream != null)
                    resStream.Close();
            }

        }
        #endregion

        [Guid("6D5140C1-7436-11CE-8034-00AA006009FA")]
        [InterfaceType(1)]
        public interface IServiceProvider
        {
            int QueryService(ref Guid guidService, ref Guid riid, out IntPtr ppvObject);
        }

        #region Implementation of IObjectWithSite
        int IObjectWithSite.SetSite(object site)
        {
            if (_prefs == null)
                _prefs = new Db(this);

            // Only need to check for install once per run.
            if (!_installChecked)
            {
                CheckInstall();
                CheckUpdate();
            }

            this._site = site;
            normalLoad = true;
            refresh = false;

            if (site != null)
            {
                var serviceProv = (IServiceProvider)this._site;
                var guidIWebBrowserApp = Marshal.GenerateGuidForType(typeof(IWebBrowserApp));
                var guidIWebBrowser2 = Marshal.GenerateGuidForType(typeof(IWebBrowser2));
                IntPtr intPtr;
                serviceProv.QueryService(ref guidIWebBrowserApp, ref guidIWebBrowser2, out intPtr);

                _browser = (IWebBrowser2)Marshal.GetObjectForIUnknown(intPtr);

                ((DWebBrowserEvents2_Event)_browser).DocumentComplete +=
                    new DWebBrowserEvents2_DocumentCompleteEventHandler(this.Run);
                if (_prefs.Settings.RunOnPageRefresh)
                {
                    ((DWebBrowserEvents2_Event)_browser).NavigateComplete2 += NavigateComplete2;
                    ((DWebBrowserEvents2_Event)_browser).DownloadComplete += DownloadComplete;
                }
            }
            else
            {
                // No site. Remove handler
                ((DWebBrowserEvents2_Event)_browser).DocumentComplete -=
                    new DWebBrowserEvents2_DocumentCompleteEventHandler(this.Run);
                if (_prefs.Settings.RunOnPageRefresh)
                {
                    ((DWebBrowserEvents2_Event)_browser).NavigateComplete2 -= NavigateComplete2;
                    ((DWebBrowserEvents2_Event)_browser).DownloadComplete -= DownloadComplete;
                }
                _browser = null;
            }
            return 0;
        }

        void NavigateComplete2(object pDisp, ref object URL)
        {
            if (pDisp != null)
                _browser = (IWebBrowser2)pDisp;
            currentURL = URL;
        }

        private void RefreshHandler(IHTMLEventObj e)
        {
            // Refresh event caught in here.
            if (refresh)
            {
                try
                {
                    Run(_browser, ref currentURL);
                }
                catch (Exception) { }
            }
            refresh = true;

        }

        private void DownloadComplete()
        {
            HTMLDocument doc = _browser.Document as HTMLDocument;
            if (doc != null && !normalLoad)
            {
                IHTMLWindow2 tmpWindow = doc.parentWindow;
                if (tmpWindow != null)
                {
                    HTMLWindowEvents2_Event events = (tmpWindow as HTMLWindowEvents2_Event);
                    try
                    {
                        if (events != null)
                        {
                            events.onload -= new HTMLWindowEvents2_onloadEventHandler(RefreshHandler);
                        }
                    }
                    catch (Exception) { }

                    try
                    {
                        events.onload += new HTMLWindowEvents2_onloadEventHandler(RefreshHandler);
                    }
                    catch (Exception) { }
                }
            }
        }

        int IObjectWithSite.GetSite(ref Guid guid, out IntPtr ppvSite)
        {
            IntPtr punk = Marshal.GetIUnknownForObject(_browser);
            int hr = Marshal.QueryInterface(punk, ref guid, out ppvSite);
            Marshal.Release(punk);
            return hr;
        }
        #endregion

        #region Implementation of IOleCommandTarget
        int IOleCommandTarget.QueryStatus(IntPtr pguidCmdGroup, uint cCmds, ref Olecmd prgCmds, IntPtr pCmdText)
        {
            return 0;
        }
        int IOleCommandTarget.Exec(IntPtr pguidCmdGroup, uint nCmdId, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            try
            {
                if (!_installChecked) // Prevent loading if not installed
                    return 0;

                ShowOptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + "Stack: " + Environment.NewLine + ex.StackTrace + Environment.NewLine + ": IOleCommandTarget.Exec", Resources.Title);
            }

            return 0;
        }
        #endregion

        #region Registering with regasm
        public static string RegBho = "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Browser Helper Objects";
        public static string RegCmd = "Software\\Microsoft\\Internet Explorer\\Extensions";

        [ComRegisterFunction]
        public static void RegisterBho(Type type)
        {
            string guid = type.GUID.ToString("B");

            // BHO
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(RegBho, true);
                if (registryKey == null)
                    registryKey = Registry.LocalMachine.CreateSubKey(RegBho);
                RegistryKey key = registryKey.OpenSubKey(guid);
                if (key == null)
                    key = registryKey.CreateSubKey(guid);
                key.SetValue("NoExplorer", 1);
                registryKey.Close();
                key.Close();
            }

            // Command
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(RegCmd, true) ??
                                          Registry.LocalMachine.CreateSubKey(RegCmd);
                RegistryKey key = registryKey.OpenSubKey(guid) ?? registryKey.CreateSubKey(guid);
                key.SetValue("ButtonText", "Manage Userscripts");
                key.SetValue("CLSID", "{1FBA04EE-3024-11d2-8F1F-0000F87ABD16}");
                key.SetValue("ClsidExtension", guid);
                key.SetValue("Icon", AssemblyPath() + ",1");
                key.SetValue("HotIcon", AssemblyPath() + ",1");
                key.SetValue("Default Visible", "Yes");
                key.SetValue("MenuText", "&Manage Userscripts");
                key.SetValue("ToolTip", "Manage ScriptMonkey Userscripts");
                //key.SetValue("KeyPath", "no");
                registryKey.Close();
                key.Close();
            }
        }

        [ComUnregisterFunction]
        public static void UnregisterBho(Type type)
        {
            string guid = type.GUID.ToString("B");
            // BHO
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(RegBho, true);
                if (registryKey != null)
                    registryKey.DeleteSubKey(guid, false);
            }
            // Command
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(RegCmd, true);
                if (registryKey != null)
                    registryKey.DeleteSubKey(guid, false);
            }
        }
        #endregion

        /// <summary>
        /// Show the options window then save settings.
        /// </summary>
        private void ShowOptions()
        {
            _prefs.LoadData();
            Options form = new Options(_prefs);
            if (form.ShowDialog() != DialogResult.Cancel)
            {
                _prefs = form.Prefs;
                _prefs.Save();
                _prefs.ReloadData();
                if (_prefs.Settings.RefreshOnSave)
                    try
                    {
                        // Refresh the page (run on refresh is disabled by default so navigate to the current location)
                        ((HTMLDocument)(_browser.Document)).parentWindow.execScript("window.location.href = window.location.href;");
                    }
                    catch (Exception ex) {
                        Log(ex, "Try refresh after save");
                    }
            }
            form.Dispose();
        }

        #region Implementation of IExtension
        /// <summary>
        /// GM_setValue
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="value">Value</param>
        /// <param name="scriptIndex">Index of script</param>
        public void setScriptValue(string name, string value, int scriptIndex)
        {
            try
            {
                if (!_prefs[scriptIndex].SavedValues.ContainsKey(name))
                    _prefs[scriptIndex].SavedValues.Add(name, value);
                else
                    _prefs[scriptIndex].SavedValues[name] = value;
                _prefs.Save();
            }
            catch (Exception ex) {
                Log(ex, "\r\nAt: setScriptValue");
            }
        }

        /// <summary>
        /// GM_getValue
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="defaultValue">Value to return if not set.</param>
        /// <param name="scriptIndex">Index of script</param>
        /// <returns></returns>
        public string getScriptValue(string name, string defaultValue, int scriptIndex)
        {
            try
            {
                return _prefs[scriptIndex].SavedValues[name];
            }
            catch (Exception ex)
            {
                Log(ex, "\r\nAt: getScriptValue");
            }
            return defaultValue;
        }

        /// <summary>
        /// GM_deleteValue
        /// </summary>
        /// <param name="name">Name of value to delete</param>
        /// <param name="scriptIndex">Index of script</param>
        public void deleteScriptValue(string name, int scriptIndex)
        {
            try
            {
                _prefs[scriptIndex].SavedValues.Remove(name);
                _prefs.Save();
            }
            catch (Exception ex)
            {
                Log(ex, "\r\nAt: deleteScriptValue");
            }
        }

        /// <summary>
        /// GM_listValues
        /// </summary>
        /// <param name="scriptIndex">Index of script</param>
        /// <returns>A comma-separated list of stored value names</returns>
        public string getScriptValuesList(int scriptIndex)
        {
            try
            {
                if (_prefs[scriptIndex].SavedValues.Count == 0)
                    return String.Empty;

                string o = String.Empty;
                foreach (KeyValuePair<string, string> item in _prefs[scriptIndex].SavedValues)
                {
                    o += item.Key;
                    o += ',';
                }
                return o.Substring(0, o.Length - 1);
            }
            catch (Exception ex)
            {
                Log(ex, "\r\nAt: getScriptValuesList");
            }
            return null;
        }

        /// <summary>
        /// GM_setClipboard
        /// </summary>
        /// <param name="data"></param>
        public void setClipboard(object data)
        {
            System.Windows.Forms.Clipboard.SetDataObject(data);
        }

        /// <summary>
        /// GM_getResourceText
        /// </summary>
        /// <param name="resourceName">Name of the resource defined in @resource</param>
        /// <param name="scriptIndex">Index of script</param>
        /// <returns>Contents of the resource</returns>
        public string getScriptResourceText(string resourceName, int scriptIndex)
        {
            try
            {
                string filepath = ResourcePath + _prefs[scriptIndex].Path + '.' + resourceName;
                if (!File.Exists(filepath))
                {
                    WebClient webClient = new WebClient();
                    try
                    {
                        webClient.DownloadFile(_prefs[scriptIndex].Resources[resourceName], filepath);
                        webClient.Dispose();
                    }
                    catch (Exception ex)
                    {
                        webClient.Dispose();
                        Log(ex, "getScriptResourceText: Error downloading file");
                        return null;
                    }
                }
                
                StreamReader str = new StreamReader(filepath);
                string o = null;
                try
                {
                    o = str.ReadToEnd();
                }
                catch (Exception ex)
                {
                    Log(ex, "getScriptResourceText: Error reading from file");
                }
                str.Close();
                return o;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// GM_getResourceURL
        /// </summary>
        /// <param name="resourceName">Name of the resource defined in @resource</param>
        /// <param name="scriptIndex">Index of script</param>
        /// <returns>The URL of the resource</returns>
        public string getScriptResourceUrl(string resourceName, int scriptIndex)
        {
            try 
	        {	        
		        return _prefs[scriptIndex].Resources[resourceName];
	        }
	        catch (Exception)
	        {
                return null;
	        }
        }

        /// <summary>
        /// Gets the current version of Scriptmonkey
        /// </summary>
        /// <returns>The current version of Scriptmonkey</returns>
        public string getVersion()
        {
            return CurrentVersion().ToString();
        }

        /// <summary>
        /// Gets the number of scripts installed
        /// </summary>
        /// <returns>The number of scripts installed</returns>
        public int getScriptCount()
        {
            return _prefs.AllScripts.Count;
        }

        /// <summary>
        /// Displays the options window
        /// </summary>
        public void showOptions() { ShowOptions(); }
        #endregion
    }
}