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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BHOUserScript.Properties;

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
 * See the issues list on GitHub if you would like to contribute.
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
        private object _currentUrl;
        private bool _refresh;
        private bool _normalLoad = true;
        private string[] _apiKeys;
        private Dictionary<string, string> _scriptCache = new Dictionary<string, string>();
        private int _refreshCounter;
        private ScriptmonkeyLinkManager _link;

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
                try
                {
                    MessageBox.Show(Resources.FirstTimeSetup, Resources.Title);

                    if (!Directory.Exists(InstallPath))
                        Directory.CreateDirectory(InstallPath);

                    if (!Directory.Exists(ScriptPath))
                        Directory.CreateDirectory(ScriptPath);

                    if (!Directory.Exists(ResourcePath))
                        Directory.CreateDirectory(ResourcePath);

                    File.Create(InstalledFile).Dispose();

                    SettingsFile s = new SettingsFile
                    {
                        BhoCreatedVersion = CurrentVersion(),
                        LastUpdateCheckDate = DateTime.Now
                    };

                    if (File.Exists(SettingsFile))
                        File.Delete(SettingsFile);

                    Db.WriteFile(SettingsFile, JsonConvert.SerializeObject(s));
                    MessageBox.Show(Resources.FirstTimeSetupDone, Resources.Title);
                }
                catch (Exception ex)
                {
                    if (LogAndCheckDebugger(ex, "Installer"))
                        throw;
                }
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
            _currentUrl = url;

            if ((_browser.Document as IHTMLDocument2) == null) // Prevents run from Windows Explorer
                return;

            if (_browser.Document == null) // No point running if no document is being displayed
                return;

            try
            {

                if (pDisp != _site)
                    return;

                if (_prefs == null)
                    _prefs = new Db(this);

                if (_prefs.Settings.UseScriptmonkeyLink)
                    _link.TickAsync();

                _refresh = true;
                if (_prefs.Settings.RunOnPageRefresh)
                    _normalLoad = false;

                if (_refreshCounter >= _prefs.Settings.ReloadAfterPages)
                {
                    if (_prefs.AllScripts.Count > 10) // Lots of scripts?
                        _prefs.ReloadDataAsync(); // Reload asynchronously to prevent browser becoming unresponsive
                    else
                        _prefs.ReloadData();
                    _refreshCounter = 0;
                }
                else
                    _refreshCounter++;

                if (!_prefs.Settings.Enabled) return;

                // Set up API keys with random length for each script
                if (_apiKeys == null || _apiKeys.Length != _prefs.AllScripts.Count)
                {
                    _apiKeys = new string[_prefs.AllScripts.Count];
                    Random r = new Random();
                    for (int i = 0; i < _apiKeys.Length; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int f = 0; f < r.Next(5,20); f++)
                        {
                            sb.Append(Convert.ToChar(r.Next(97,122)));
                        }
                        _apiKeys[i] = sb.ToString();
                    }
                }

                var document2 = _browser.Document as HTMLDocument;
                //var document3 = browser.Document as IHTMLDocument3;

                if (document2 == null) return;

                if (url.ToString().StartsWith("javascript:") || document2.url.StartsWith("res://"))
                    return;

                var window = document2.parentWindow;

                if (window == null)
                    return;

                if (_prefs.Settings.InjectAPI)
                    SetupWindow(window);

                if (document2.url.Contains("https://servc.eu/p/scriptmonkey/"))
                    try
                    {
                        // Tell webpage Scriptmonkey is installed
                        var v = CurrentVersion();
                        window.execScript(
                            $"ld_Scriptmonkey_Installed({v.Major},{v.Minor},{v.Revision},{_prefs.AllScripts.Count});");
                    }
                    catch (Exception ex)
                    {
                        if (LogAndCheckDebugger(ex, "Unable to inject JavaScript into webpage"))
                            throw;
                    }

                if (document2.url == "https://servc.eu/p/scriptmonkey/options.html")
                    ShowOptions();
                    
                var menuContent = "<div style=\"" + _prefs.Settings.MenuCommandCSS.Replace('"','\'') + "\">"; // Replace double quotes with single to prevent escaping out of style
                bool useMenuCommands = false;

                for (int i = 0; i < _prefs.AllScripts.Count; i++)
                {
                    TryCheckRunScript(i, url.ToString(), window, ref useMenuCommands, ref menuContent);
                }

                menuContent += "</div>";
                if (useMenuCommands)
                    new Thread(() => { document2.body.insertAdjacentHTML("afterbegin", menuContent); }).Start();
            }
            catch (Exception ex)
            {
                if (LogAndCheckDebugger(ex, "At: main"))
                    throw;
                CheckInstall(); // Error may be caused by invalid installation. Verify files haven't been deleted.
            }
        }

        /// <summary>
        /// Try to check if the script can be run in the current browser window and run it
        /// </summary>
        /// <param name="i">The index of the script in the settings file</param>
        /// <param name="url">The current URL</param>
        /// <param name="window">The current IHTMLWindow2</param>
        /// <param name="useMenuCommands">Should the menu commands HTML be injected into the page?</param>
        /// <param name="menuContent">The content for the menu commands HTML</param>
        private void TryCheckRunScript(int i, string url, IHTMLWindow2 window, ref bool useMenuCommands, ref string menuContent)
        {
            if (!_prefs[i].Enabled) return;

            if (_prefs[i].Include.Length > 0 && !Regex.IsMatch(url, WildcardToRegex(_prefs[i].Include)))
                return;

            if (_prefs[i].Exclude.Length > 0 && Regex.IsMatch(url, WildcardToRegex(_prefs[i].Exclude)))
                return;

                string scriptContent = String.Empty;

            try
            {
                if (_prefs.Settings.CacheScripts && _scriptCache.ContainsKey(_prefs[i].Path))
                    scriptContent = _scriptCache[_prefs[i].Path];
                else
                {
                    //var str = new StreamReader(ScriptPath + _prefs[i].Path);
                    //scriptContent = str.ReadToEnd();
                    //str.Close();
                    scriptContent = _prefs.ReadFile(ScriptPath + _prefs[i].Path);

                    if (_prefs.Settings.CacheScripts)
                        _scriptCache.Add(_prefs[i].Path, scriptContent);
                }

                var content = "function Scriptmonkey_S" + i + "_proto() {";
                if (_prefs.Settings.InjectAPI)
                    content += Resources.WrapperJS_Before + i + Resources.WrapperJS_Mid + _apiKeys[i] +
                               Resources.WrapperJS_After + scriptContent;
                else
                    content += scriptContent;

                if (_prefs[i].MenuCommands?.Count > 0)
                {
                    foreach (NameFunctionPair command in _prefs[i].MenuCommands)
                    {
                        var internalName = GenerateRandomString();
                        content += "this.SM_" + internalName + " = " + command.Function + ";";
                        menuContent += "<p><a style=\"cursor: pointer; color: #4495d4;\" onclick=\"Scriptmonkey_S" + i + ".SM_" + internalName + "();\">" + command.Name + "</a></p>";
                    }
                    useMenuCommands = true;
                }

                content += "}var Scriptmonkey_S" + i + " = new Scriptmonkey_S" + i + "_proto();";

                //RunScript(content, window, _prefs[i].Name);
                var f = i;
                var t = new Thread(() => RunScript(content, window, _prefs[f].Name));
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
            }
            catch (Exception ex)
            {
                window.execScript("console.log(\"Scriptmonkey: Unable to load script: " + _prefs[i].Name + ". Error: " + ex.Message.Replace("\"", "\\\"") + "\");");
                bool shouldThrow;
                if (_prefs.Settings.LogScriptContentsOnRunError && !ex.Message.Contains("Access is denied"))
                    shouldThrow = LogAndCheckDebugger(ex, "At script: " + _prefs[i].Name + ':' + Environment.NewLine + scriptContent);
                else
                    shouldThrow = LogAndCheckDebugger(ex, "At script: " + _prefs[i].Name);

                if (shouldThrow)
                    throw;

            }
        }

        /// <summary>
        /// Exposes the IExtension interface to the browser window.
        /// </summary>
        /// <param name="window"></param>
        private void SetupWindow(dynamic window)
        {
            try
            {
                var exp = (IExpando)window;
                var info = exp.AddProperty("Scriptmonkey");
                info.SetValue(exp, this);
            }
            catch (Exception ex)
            {
                if (LogAndCheckDebugger(ex, "SetupWindow"))
                    throw;
            }
        }
        
        /// <summary>
        /// Executes JavaScript on the window
        /// </summary>
        /// <param name="content">The JavaScript to execute.</param>
        /// <param name="window">The window.</param>
        /// <param name="name">Name of the script.</param>
        private void RunScript(string content, IHTMLWindow2 window, string name)
        {
            try
            {
                window.execScript(content);
            }
            catch (Exception ex)
            {
                //window.execScript("console.log(\"Scriptmonkey: Unable to load script: " + name + ". Error: " +
                //                    ex.Message.Replace("\"", "\\\"") + "\");");
                bool shouldThrow;
                if (_prefs.Settings.LogScriptContentsOnRunError && !ex.Message.Contains("Access is denied"))
                    shouldThrow = LogAndCheckDebugger(ex, "At script: " + name + ':' + Environment.NewLine + content);
                else
                    shouldThrow = LogAndCheckDebugger(ex, "At script: " + name);

                if (shouldThrow)
                    throw;
            }
        }

        /// <summary>
        /// Ask user if they want to install a script
        /// </summary>
        /// <param name="url">The URL of the script to download</param>
        /// <returns>True if script install attempted</returns>
        private bool AskInstallScript(string url)
        {
            AddScriptUrlFrm form = new AddScriptUrlFrm();
            if (form.ShowDialog() == DialogResult.OK) // Automatically
            {
                try
                {
                    string relativeScriptPath = GenerateRandomString() + ".user.js";
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(url, ScriptPath
                        + relativeScriptPath);
                    webClient.Dispose();
                    var s = ParseScriptMetadata.Parse(relativeScriptPath);
                    s.Path = relativeScriptPath;
                    _prefs.AddScript(s);
                }
                catch (Exception ex)
                {
                    if (LogAndCheckDebugger(ex, Resources.AutomaticAddFailError))
                        throw;
                }
                return true;
            }
            form.Dispose();
            return false;
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
        private static string AssemblyPath() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "Scriptmonkey.dll";

        /// <summary>
        /// Convert a string array of @match values into Regex.
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns>Regex pattern</returns>
        private static string WildcardToRegex(string[] pattern)
        {
            var _out = String.Empty;
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
        /// Writes an exception to the log file. Returns true if compiled in debug configuration and an exception should be thrown.
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="extraInfo">Any extra info (debug notes, vars)</param>
        /// <returns>If the exception should be thrown</returns>
        public static bool LogAndCheckDebugger(Exception ex, string extraInfo = null)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(InstallPath + "log.txt"))
                {
                    if (ex != null)
                    {
                        writer.WriteLine(ex.Message);
                        writer.WriteLine(ex.StackTrace);
                    }
                    if (extraInfo != null) 
                        writer.WriteLine(extraInfo);
                    writer.WriteLine("------------ " + DateTime.Now);
                }
            }
            catch (Exception) { }
#if DEBUG
            var shouldDebug = MessageBox.Show(ex?.Message + Environment.NewLine + @"Stack: " + Environment.NewLine + ex?.StackTrace +
                                Environment.NewLine + @"Souce: " + Environment.NewLine + ex?.Source + @": Main" +
                                ((extraInfo != null) ? Environment.NewLine + extraInfo : ""),
                                Resources.Title + @": Debug?", MessageBoxButtons.YesNo);
            
            if (shouldDebug == DialogResult.Yes && ex != null)
            {
                if (!Debugger.IsAttached)
                    Debugger.Launch();
                return true;
            }
#endif
            return false;
        }

        /// <summary>
        /// Check for updates from a URL, then prompt the user to install them.
        /// </summary>
        private void CheckUpdate()
        {
            if (_prefs.Settings.CheckForUpdates && _prefs.Settings.LastUpdateCheckDate < DateTime.Now - TimeSpan.FromDays(3))
            {
                try
                {
                    UpdateResponse response = JsonConvert.DeserializeObject<UpdateResponse>(SendWebRequest("https://servc.eu/p/scriptmonkey/version.php", true));

                    if (response.Success && response.LatestVersion > CurrentVersion())
                    {
                        // Got valid response from server and there is an update. Now to ask user to update.
                        var form = new UpdateBHOFrm
                        {
                            currentVersionTxt = {Text = CurrentVersion().ToString()},
                            newVersionTxt = {Text = response.LatestVersion.ToString()},
                            textBox1 = {Text = response.Changes}
                        };
                        form.ShowDialog();

                        if (form.Response != UpdateBHOFrm.UpdateBHOFrmResponse.NextTime)
                        {
                            _prefs.Settings.LastUpdateCheckDate = DateTime.Now;
                            _prefs.Save(true);
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
                        _prefs.Save(true);
                    }
                }
                catch (Exception) { }
            }

        }

        /// <summary>
        /// If the time since install of each script is greater than 7 days and an update URL is set, check for updates
        /// </summary>
        private void CheckScriptUpdate()
        {
            DateTime now = DateTime.UtcNow;
            var updated = false;
            var toUpdate = new List<Script>();
            var toUpdateTo = new List<ScriptWithContent>();
            foreach (Script s in _prefs.AllScripts)
            {
                if (!String.IsNullOrWhiteSpace(s.UpdateUrl) && s.InstallDate < now - TimeSpan.FromDays(7))
                {
                    try
                    {
                        var content = SendWebRequest(s.UpdateUrl);
                        var newScript = ParseScriptMetadata.ParseFromContents(content);
                        if (newScript.Version != s.Version)
                        {
                            var scriptContents = new ScriptWithContent(newScript) {Content = content};
                            toUpdate.Add(s);
                            toUpdateTo.Add(scriptContents);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (LogAndCheckDebugger(ex, "Script update check failed for: " + s.Name))
                            throw;
                    }
                    s.InstallDate = now;
                    updated = true;
                }
            }

            if (updated)
            {
                _prefs.Save();
            }

            if (toUpdate.Count > 0)
                AskUpdateScriptsAsync(toUpdate, toUpdateTo);
        }

        /// <summary>
        /// Ask the user wheather to update the scripts
        /// </summary>
        /// <param name="toUpdate">List of the currently installed scripts</param>
        /// <param name="toUpdateTo">List of the new script objects with content. Must have the same length as toUpdate</param>
        private void AskUpdateScriptsAsync(List<Script> toUpdate, List<ScriptWithContent> toUpdateTo)
        {
            Thread t = new Thread(() =>
            {
                var frm = new AskUpdateScriptFrm();
                for (var i = 0; i < toUpdate.Count; i++)
                {
                    frm.listBox1.Items.Add($"{toUpdate[i].Name} - {toUpdate[i].Version} to {toUpdateTo[i].ScriptData.Version}");
                }
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                if (toUpdate.Count > 0)
                {
                    for (int i = 0; i < toUpdate.Count; i++)
                    {
                        // Delete existing backups
                        if (File.Exists(ScriptPath + toUpdate[i].Path + @".backup"))
                            File.Delete(ScriptPath + toUpdate[i].Path + @".backup");

                        // Move current file to backup
                        File.Move(ScriptPath + toUpdate[i].Path, ScriptPath + toUpdate[i].Path + @".backup");

                        try
                        {
                            // Save updated script
                            Db.WriteFile(ScriptPath + toUpdate[i].Path, toUpdateTo[i].Content);
                        }
                        catch (Exception ex)
                        {
                            if (LogAndCheckDebugger(ex, "Unable to update script: " + toUpdate[i].Name))
                                throw;

                            // Something went wrong. Restore from backup
                            if (File.Exists(ScriptPath + toUpdate[i].Path))
                                File.Delete(ScriptPath + toUpdate[i].Path);

                            File.Copy(ScriptPath + toUpdate[i].Path + @".backup", ScriptPath + toUpdate[i].Path);
                        }

                        // Remove from cache
                        if (_scriptCache.ContainsKey(toUpdate[i].Path))
                            _scriptCache.Remove(toUpdate[i].Path);

                        // Update stored script with metadata of script updated to
                        for (int j = 0; j < _prefs.AllScripts.Count; j++)
                        {
                            if (_prefs[j].Path == toUpdate[i].Path)
                            {
                                toUpdateTo[i].ScriptData.Path = toUpdate[i].Path;
                                _prefs[j] = toUpdateTo[i].ScriptData;
                                break;
                            }
                        }
                    }
                    _prefs.Save(true);
                    _prefs.ReloadData();
                }
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        /// <summary>
        /// Send a WebRequest (with a timeout of 1000ms). Returns an empty string on timeout.
        /// </summary>
        /// <param name="url">The requested resource</param>
        /// <param name="quick">True to set a timeout of 1000ms</param>
        /// <returns>The requested resource</returns>
        public static string SendWebRequest(string url, bool quick = false)
        {
            HttpWebRequest wc = (HttpWebRequest)WebRequest.Create(new Uri(url));
            if (quick)
                wc.Timeout = 1000;
            byte[] buf = new byte[8192];
            StringBuilder sb = new StringBuilder();
            try
            {
                WebResponse wr = wc.GetResponse();
                Stream resStream = wr.GetResponseStream();

                int? count;
                do
                {
                    count = resStream?.Read(buf, 0, buf.Length);
                    if (count == null)
                        break;
                    if (count != 0)
                    {
                        sb.Append(Encoding.ASCII.GetString(buf, 0, (int) count));
                    }
                } while (count > 0);

                wr.Close();
                resStream?.Close();
            }
            catch (Exception ex)
            {
                if (!(ex is WebException) && LogAndCheckDebugger(ex, "URL: " + url))
                    throw;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Generate a random number to use as a script prefix and avoid duplicate installs
        /// </summary>
        /// <returns>A random number from 0 to 100,000</returns>
        public static string GenerateRandomString()
        {
            Random r = new Random();
            return (char)(r.Next(97, 122)) + Convert.ToString(r.Next(100000));
        }

        /// <summary>
        /// Event handler for Scriptmonkey Link
        /// </summary>
        /// <param name="action">The action to perform</param>
        private void OnReceiveLinkEvent(string action)
        {
            if (action == "refresh")
                _prefs.ReloadData();
            else if (action == "disableLink")
            {
                _link.Dispose();
                _link = null;
            }
            else if (action == "testLink")
                LogAndCheckDebugger(null, "Scriptmonkey Link alert: " + (_currentUrl ?? ""));
            else if (action == "verify")
                _link.Verify(_currentUrl.ToString());
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

            if (_link == null)
            {
                _link = new ScriptmonkeyLinkManager();
                _link.OnReceiveEvent += OnReceiveLinkEvent;
            }

            // Only need to check for install once per run.
            if (!_installChecked)
            {
                CheckInstall();

                var u = new Thread(CheckUpdate);
                u.SetApartmentState(ApartmentState.STA);
                u.Start();

                var s = new Thread(CheckScriptUpdate);
                s.SetApartmentState(ApartmentState.STA);
                s.Start();
            }

            _site = site;
            _normalLoad = true;
            _refresh = false;

            if (!Application.RenderWithVisualStyles)
                Application.EnableVisualStyles();

            if (site != null)
            {
                var serviceProv = (IServiceProvider)_site;
                var guidIWebBrowserApp = Marshal.GenerateGuidForType(typeof(IWebBrowserApp));
                var guidIWebBrowser2 = Marshal.GenerateGuidForType(typeof(IWebBrowser2));
                IntPtr intPtr;
                serviceProv.QueryService(ref guidIWebBrowserApp, ref guidIWebBrowser2, out intPtr);

                _browser = (IWebBrowser2)Marshal.GetObjectForIUnknown(intPtr);

                ((DWebBrowserEvents2_Event)_browser).DocumentComplete += Run;
                ((DWebBrowserEvents2_Event)_browser).BeforeNavigate2 += BeforeNavigate;

                if (_prefs.Settings.RunOnPageRefresh)
                {
                    ((DWebBrowserEvents2_Event)_browser).NavigateComplete2 += NavigateComplete2;
                    ((DWebBrowserEvents2_Event)_browser).DownloadComplete += DownloadComplete;
                }
            }
            else
            {
                // No site. Remove handlers
                if (_browser != null)
                {
                    ((DWebBrowserEvents2_Event) _browser).DocumentComplete -= Run;
                    ((DWebBrowserEvents2_Event) _browser).BeforeNavigate2 -= BeforeNavigate;
                    if (_prefs.Settings.RunOnPageRefresh)
                    {
                        ((DWebBrowserEvents2_Event) _browser).NavigateComplete2 -= NavigateComplete2;
                        ((DWebBrowserEvents2_Event) _browser).DownloadComplete -= DownloadComplete;
                    }
                    _browser = null;
                }
            }
            return 0;
        }

        private void BeforeNavigate(object pDisp, ref object url, ref object flags, ref object targetFrameName, ref object postData, ref object headers, ref bool cancel)
        {
            if (pDisp != _site)
                return;

            if (_prefs.Settings.AutoDownloadScripts && url.ToString().Contains(".user.js"))
                cancel = AskInstallScript(url.ToString());
        }

        void NavigateComplete2(object pDisp, ref object url)
        {
            if (pDisp != null)
                _browser = (IWebBrowser2)pDisp;
            _currentUrl = url;
        }

        private void RefreshHandler(IHTMLEventObj e)
        {
            // Refresh event caught in here.
            if (_refresh)
            {
                try
                {
                    Run(_browser, ref _currentUrl);
                }
                catch (Exception) { }
            }
            _refresh = true;

        }

        private void DownloadComplete()
        {
            HTMLDocument doc = _browser.Document as HTMLDocument;
            if (doc == null || _normalLoad) return;

            IHTMLWindow2 tmpWindow = doc.parentWindow;
            if (tmpWindow == null) return;

            HTMLWindowEvents2_Event events = (tmpWindow as HTMLWindowEvents2_Event);
            try
            {
                if (events != null)
                {
                    events.onload -= RefreshHandler;
                }
            }
            catch (Exception) { }

            try
            {
                events.onload += RefreshHandler;
            }
            catch (Exception) { }
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
                MessageBox.Show(ex.Message + Environment.NewLine + @"Stack: " + Environment.NewLine + ex.StackTrace + Environment.NewLine + @": IOleCommandTarget.Exec", Resources.Title);
            }

            return 0;
        }
        #endregion

        #region Registering with regasm

        private static string RegBho = "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Browser Helper Objects";
        private static string RegCmd = "Software\\Microsoft\\Internet Explorer\\Extensions";

        private static string EpmKey = "Software\\Microsoft\\Internet Explorer\\Low Rights\\ElevationPolicy";

        [ComRegisterFunction]
        public static void RegisterBho(Type type)
        {
            string guid = type.GUID.ToString("B");

            // BHO
            {
                var registryKey = Registry.LocalMachine.OpenSubKey(RegBho, true) ??
                                  Registry.LocalMachine.CreateSubKey(RegBho);
                var key = registryKey?.OpenSubKey(guid) ?? registryKey?.CreateSubKey(guid);
                key?.SetValue("NoExplorer", 1);
                registryKey?.Close();
                key?.Close();
            }

            // Command
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(RegCmd, true) ??
                                          Registry.LocalMachine.CreateSubKey(RegCmd);
                if (registryKey != null) {
                    var key = registryKey.OpenSubKey(guid) ?? registryKey.CreateSubKey(guid);
                    key?.SetValue("ButtonText", "Manage Userscripts");
                    key?.SetValue("CLSID", "{1FBA04EE-3024-11d2-8F1F-0000F87ABD16}");
                    key?.SetValue("ClsidExtension", guid);
                    key?.SetValue("Icon", AssemblyPath() + ",1");
                    key?.SetValue("HotIcon", AssemblyPath() + ",1");
                    key?.SetValue("Default Visible", "Yes");
                    key?.SetValue("MenuText", "&Manage Userscripts");
                    key?.SetValue("ToolTip", "Manage ScriptMonkey Userscripts");
                    //key.SetValue("KeyPath", "no");
                    registryKey.Close();
                    key?.Close();
                }

                // Enhanced Protected Mode
                {
                    RegistryKey regKey = Registry.LocalMachine.OpenSubKey(EpmKey, true) ??
                                              Registry.LocalMachine.CreateSubKey(EpmKey);
                    if (regKey != null)
                    {
                        var key = regKey.OpenSubKey(guid) ?? regKey.CreateSubKey(guid);

                        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                        if (path == null)
                            path = String.Empty;

                        key?.SetValue("AppName", "Scriptmonkey.dll");
                        key?.SetValue("AppPath", path);
                        key?.SetValue("Policy", 3);
                        regKey.Close();
                        key?.Close();
                    }
                }
            }
        }

        [ComUnregisterFunction]
        public static void UnregisterBho(Type type)
        {
            string guid = type.GUID.ToString("B");
            // BHO
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(RegBho, true);
                registryKey?.DeleteSubKey(guid, false);
            }
            // Command
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(RegCmd, true);
                registryKey?.DeleteSubKey(guid, false);
            }
            // Enhanced Protected Mode
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(EpmKey, true);
                registryKey?.DeleteSubKey(guid, false);
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
                _prefs.Save(true);
                _prefs.ReloadData();
                _refreshCounter = 0;
                if (_prefs.Settings.RefreshOnSave)
                {
                    try
                    {
                        // Refresh the page (run on refresh is disabled by default so navigate to the current location)
                        ((HTMLDocument)_browser.Document).parentWindow.execScript(
                            "window.location.href = window.location.href;");
                    }
                    catch (Exception ex)
                    {
                        if (LogAndCheckDebugger(ex, "Try refresh after save"))
                            throw;
                    }
                }
                _link?.SendAction("refresh");
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
        /// <param name="apiKey">The API key for the specified script.</param>
        public void setScriptValue(string name, string value, int scriptIndex, string apiKey)
        {
            if (!CheckScriptApiKey(scriptIndex, apiKey))
                return;

            try
            {
                if (_prefs[scriptIndex].SavedValues == null)
                    _prefs[scriptIndex].SavedValues = new Dictionary<string, string>();

                if (!_prefs[scriptIndex].SavedValues.ContainsKey(name))
                    _prefs[scriptIndex].SavedValues.Add(name, value);
                else
                    _prefs[scriptIndex].SavedValues[name] = value;
                _prefs.Save();
            }
            catch (Exception ex) {
                if (LogAndCheckDebugger(ex, "\r\nAt: setScriptValue"))
                    throw;
            }
        }

        /// <summary>
        /// GM_getValue
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="defaultValue">Value to return if not set.</param>
        /// <param name="scriptIndex">Index of script</param>
        /// <param name="apiKey">The API key for the specified script.</param>
        /// <returns></returns>
        public string getScriptValue(string name, string defaultValue, int scriptIndex, string apiKey)
        {
            if (!CheckScriptApiKey(scriptIndex, apiKey))
                return null;

            try
            {
                string o = defaultValue;
                if (_prefs[scriptIndex].SavedValues?.TryGetValue(name, out o) == true)
                    return o;
            }
            catch (Exception ex)
            {
                if (LogAndCheckDebugger(ex, "\r\nAt: getScriptValue"))
                    throw;
            }
            return defaultValue;
        }

        /// <summary>
        /// GM_deleteValue
        /// </summary>
        /// <param name="name">Name of value to delete</param>
        /// <param name="scriptIndex">Index of script</param>
        /// <param name="apiKey">The API key for the specified script.</param>
        public void deleteScriptValue(string name, int scriptIndex, string apiKey)
        {
            if (!CheckScriptApiKey(scriptIndex, apiKey))
                return;

            try
            {
                _prefs[scriptIndex].SavedValues.Remove(name);
                _prefs.Save();
            }
            catch (Exception ex)
            {
                if (LogAndCheckDebugger(ex, "\r\nAt: deleteScriptValue"))
                    throw;
            }
        }

        /// <summary>
        /// GM_listValues
        /// </summary>
        /// <param name="scriptIndex">Index of script</param>
        /// <param name="apiKey">The API key for the specified script.</param>
        /// <returns>A comma-separated list of stored value names</returns>
        public string getScriptValuesList(int scriptIndex, string apiKey)
        {
            if (!CheckScriptApiKey(scriptIndex, apiKey))
                return null;

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
                if (LogAndCheckDebugger(ex, "\r\nAt: getScriptValuesList"))
                    throw;
            }
            return null;
        }

        /// <summary>
        /// GM_setClipboard
        /// </summary>
        /// <param name="data">Clipboard data</param>
        public void setClipboard(object data)
        {
            Clipboard.SetDataObject(data);
        }

        /// <summary>
        /// GM_getResourceText
        /// </summary>
        /// <param name="resourceName">Name of the resource defined in @resource</param>
        /// <param name="scriptIndex">Index of script</param>
        /// <param name="apiKey">The API key for the specified script.</param>
        /// <returns>Contents of the resource</returns>
        public string getScriptResourceText(string resourceName, int scriptIndex, string apiKey)
        {
            if (!CheckScriptApiKey(scriptIndex, apiKey))
                return null;

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
                        if (LogAndCheckDebugger(ex, "getScriptResourceText: Error downloading file"))
                            throw;
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
                    if (LogAndCheckDebugger(ex, "getScriptResourceText: Error reading from file"))
                        throw;
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
        /// <param name="apiKey">The API key for the specified script.</param>
        /// <returns>The URL of the resource</returns>
        public string getScriptResourceUrl(string resourceName, int scriptIndex, string apiKey)
        {
            if (!CheckScriptApiKey(scriptIndex, apiKey))
                return null;

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
        /// GM_xmlhttpRequest
        /// </summary>
        /// <param name="details">JSON serialised XmlHttpRequestDetails</param>
        /// <param name="scriptIndex">Index of script</param>
        /// <param name="apiKey">The API key for the specified script.</param>
        /// <returns>JSON serialised XmlHttpRequestResponse</returns>
        public string xmlHttpRequest(string details, int scriptIndex, string apiKey)
        {
            if (!CheckScriptApiKey(scriptIndex, apiKey))
                return null;

            WebClient webClient = new WebClient();
            XmlHttpRequestResponse response = new XmlHttpRequestResponse();

            try
            {
                XmlHttpRequestDetails vars = JsonConvert.DeserializeObject<XmlHttpRequestDetails>(details);

                if (vars.Headers != null)
                {
                    foreach (KeyValuePair<string, string> header in vars.Headers)
                    {
                        webClient.Headers.Add(header.Key + ": " + header.Value);
                    }
                }

                if (vars.Method == "GET" || vars.Method == "HEAD")
                    response.ResponseText = webClient.DownloadString(vars.Url);
                else if (vars.Method == "POST")
                {
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    response.ResponseText = webClient.UploadString(vars.Url, vars.Data);
                }
                else // Unsupported method
                {
                    response.StatusText = "Unsupported method";
                }

                if (response.ResponseText == null)
                    response.ResponseText = String.Empty;

                response.ReadyState = 4;
                GetStatusDetails(webClient, out response.StatusText, out response.Status);

                response.FinalUrl = webClient.ResponseHeaders[HttpResponseHeader.Location] ?? vars.Url;

                response.ResponseHeaders = String.Empty;
                var resKeys = webClient.ResponseHeaders.Keys;
                for (int i = 0; i < webClient.ResponseHeaders.Count; i++)
                {
                    response.ResponseHeaders += resKeys[i] + ": " + webClient.ResponseHeaders[i] + "\r\n";
                }

            }
            catch (Exception ex)
            {

                if (!(ex is WebException) && LogAndCheckDebugger(ex, "xmlHttpRequest: Error downloading file"))
                    throw;
                response.ResponseText = ex.Message;
                GetStatusDetails(webClient, out response.StatusText, out response.Status);
            }

            webClient.Dispose();
            return JsonConvert.SerializeObject(response);
        }

        /// <summary>
        /// Gets the status information of a WebClient.
        /// </summary>
        /// <param name="client">The WebClient object</param>
        /// <param name="statusDescription">Status description</param>
        /// <param name="statusCode">The HTTP status code</param>
        /// <returns></returns>
        private static void GetStatusDetails(WebClient client, out string statusDescription, out int statusCode)
        {
            var responseField = client.GetType().GetField("m_WebResponse", BindingFlags.Instance | BindingFlags.NonPublic);

            var response = responseField?.GetValue(client) as HttpWebResponse;

            if (response != null)
            {
                statusDescription = response.StatusDescription;
                statusCode = Convert.ToInt32(response.StatusCode);
                return;
            }

            statusDescription = null;
            statusCode = 400;
        }

        public void setMenuCommand(string function, string caption, int scriptIndex, string apiKey)
        {
            if (!CheckScriptApiKey(scriptIndex, apiKey))
                return;


            if (_prefs[scriptIndex].MenuCommands == null)
                _prefs[scriptIndex].MenuCommands = new List<NameFunctionPair>();

            if (_prefs[scriptIndex].MenuCommands.Any(command => command.Function == function || command.Name == caption)) {
                return;
            }

            var p = new NameFunctionPair {Function = function, Name = caption};
            
            _prefs[scriptIndex].MenuCommands.Add(p);
            _prefs.Save();
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

        /// <summary>
        /// Checks if the provided access key is valid and supplied scriptIndex is in range.
        /// </summary>
        /// <param name="scriptIndex">The 0-based index of the script (Defines which script the API operation applies to)</param>
        /// <param name="apiKey">The API key of the script.</param>
        /// <returns>True if the API key is valid and scriptIndex is in range. False to abort API operation.</returns>
        private bool CheckScriptApiKey(int scriptIndex, string apiKey)
        {
            if (_prefs.Settings.UsePublicAPI && apiKey == "public")
                return true;

            // Check the scriptIndex isn't out of range
            if (scriptIndex >= _apiKeys.Length)
                return false;

            return _apiKeys[scriptIndex] == apiKey;
        }
        #endregion
    }
}