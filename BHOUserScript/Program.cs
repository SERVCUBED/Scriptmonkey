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
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("7f724466-7c51-4b14-80a1-7b0568b4226c")]
    [ProgId("SERVCUBED.Scriptmonkey")]
    public class Scriptmonkey : IObjectWithSite, IOleCommandTarget
    {
        #region Declarations
        IWebBrowser2 _browser;
        private object _site;
        private bool _installChecked;

        public static readonly string InstallPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
            + Path.DirectorySeparatorChar + ".Scriptmonkey" + Path.DirectorySeparatorChar;
        public static readonly string ScriptPath = InstallPath + "scripts" + Path.DirectorySeparatorChar;
        public static readonly string SettingsFile = InstallPath + "settings.json";
        public static readonly string InstalledFile = InstallPath + "installed";

        private Db _prefs;
        #endregion

        #region Is installed?
        public void CheckInstall()
        {
            try
            {
                if(!File.Exists(InstalledFile))
                {
                    MessageBox.Show(Resources.FirstTimeSetup, Resources.Title);
                    Directory.CreateDirectory(InstallPath);
                    Directory.CreateDirectory(ScriptPath);
                    File.Create(InstalledFile);

                    StreamWriter jsonDb = new StreamWriter(SettingsFile);
                    SettingsFile s = new SettingsFile();
                    s.BhoCreatedVersion = CurrentVersion();

                    jsonDb.Write(JsonConvert.SerializeObject(s)); // Write blank json settings file
                    jsonDb.Close();
                    MessageBox.Show(Resources.FirstTimeSetupDone, Resources.Title);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.Title);
            }

            _prefs.LoadData();

            // Has BHO been updated? Settings file needs to be updated
            _prefs.CheckUpdateStatus();

            _installChecked = true;
        }
        #endregion

        #region Main Function
        void Run(object pDisp, ref object url) // OnDocumentComplete handler
        {
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

                if (_prefs.AllScripts.Count > 10) // Lots of scripts?
                    _prefs.ReloadDataAsync(); // Reload asynchronously to prevent browser becoming unresponsive
                else
                    _prefs.ReloadData();

                if (_prefs.Settings.Enabled)
                {
                    var document2 = _browser.Document as IHTMLDocument2;
                    //var document3 = browser.Document as IHTMLDocument3;

                    var window = document2.parentWindow;
                    // Check if user wants to install script
                    if (document2.url.Contains(".user.js"))
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
                                MessageBox.Show(Resources.AutomaticAddFailError + ex.Message, Resources.Title);
                            }
                        }
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

                            if (shouldRun)
                            {
                                StreamReader str = new StreamReader(ScriptPath + _prefs[i].Path);
                                string c = str.ReadToEnd();
                                str.Close();
                                try
                                {
                                    window.execScript("(function(){" + c + "})();");
                                }
                                catch (Exception) { }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\nStack: \r\n" + ex.StackTrace + "\r\nSouce: \r\n" + ex.Source, Resources.Title);
                CheckInstall(); // Error may be caused by invalid installtion. Verify files haven't been deleted.
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

        public static string AssemblyPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "Scriptmonkey.dll";
        }

        public static string WildcardToRegex(string[] pattern)
        {
            string _out = "";
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
                CheckInstall();

            this._site = site;

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
            }
            else
            {
                // Disabled this handler - no point running if there is not a site displayed
                //((DWebBrowserEvents2_Event)browser).DocumentComplete -=
                //    new DWebBrowserEvents2_DocumentCompleteEventHandler(this.Run);
                _browser = null;
            }
            return 0;
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
                
                _prefs.LoadData();
                Options form = new Options(_prefs);
                if (form.ShowDialog() != DialogResult.Cancel)
                {
                    _prefs = form.Prefs;
                    _prefs.Save();
                    _prefs.ReloadDataAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\nStack: \r\n" + ex.StackTrace, Resources.Title);
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
    }
}