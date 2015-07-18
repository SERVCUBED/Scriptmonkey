using System;
using System.IO;
using Microsoft.Win32;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using mshtml;
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
        IWebBrowser2 browser;
        private object site;
        private bool installChecked = false;

        public static string installPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
            + Path.DirectorySeparatorChar + ".Scriptmonkey" + Path.DirectorySeparatorChar;
        public static string scriptPath = installPath + "scripts" + Path.DirectorySeparatorChar;
        public static string settingsFile = installPath + "settings.json";
        public static string installedFile = installPath + "installed";

        public db prefs;
        #endregion

        #region Is installed?
        public void checkInstall()
        {
            try
            {
                if(!File.Exists(installedFile))
                {
                    MessageBox.Show("Scriptmonkey is being set up for first time use. Please stand by. Your browser may appear unresponsive.", "Scriptmonkey");
                    Directory.CreateDirectory(installPath);
                    Directory.CreateDirectory(scriptPath);
                    File.Create(installedFile);

                    StreamWriter jsonDB = new StreamWriter(settingsFile);
                    SettingsFile s = new SettingsFile();

                    jsonDB.Write(JsonConvert.SerializeObject(s)); // Write blank json settings file
                    jsonDB.Close();
                    MessageBox.Show("Finished settings up Scriptmonkey. Enjoy! :)", "Scriptmonkey");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Scriptmonkey");
            }

            prefs.LoadData();

            // Has BHO been updated? Settings file needs to be updated
            prefs.CheckUpdateStatus();

            installChecked = true;
        }
        #endregion

        #region Main Function
        void Run(object pDisp, ref object URL) // OnDocumentComplete handler
        {
            if ((browser.Document as IHTMLDocument2) == null) // Prevents run from Windows Explorer
                return;

            if (browser.Document == null) // No point running if no document is being displayed
                return;

            try
            {

                if (pDisp != this.site)
                    return;

                if (prefs == null)
                    prefs = new db(this);

                if (prefs.AllScripts.Count > 10) // Lots of scripts?
                    prefs.ReloadDataAsync(); // Reload asynchronously to prevent browser becoming unresponsive
                else
                    prefs.ReloadData();

                if (prefs.Settings.Enabled)
                {
                    var document2 = browser.Document as IHTMLDocument2;
                    //var document3 = browser.Document as IHTMLDocument3;

                    var window = document2.parentWindow;
                    // Check if user wants to install script
                    if (document2.url.Contains(".user.js"))
                    {
                        AddScriptURLFrm form = new AddScriptURLFrm();
                        if (form.ShowDialog() == DialogResult.OK) // Automatically
                        {
                            try
                            {
                                System.Net.WebClient webClient = new System.Net.WebClient();
                                webClient.DownloadFile(document2.url, BHOUserScript.Scriptmonkey.scriptPath 
                                    + document2.url.Substring(document2.url.LastIndexOf('/') + 1));
                                Script s = ParseScriptMetadata.Parse(document2.url.Substring(document2.url.LastIndexOf('/')));
                                s.Path = document2.url.Substring(document2.url.LastIndexOf('/') + 1);
                                prefs.AddScript(s);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Oops! Unable to automatically install the script. Please try again or install the script manually.\r\n\r\nError:\r\n"
                                    + ex.Message, "Scriptmonkey");
                            }
                        }
                    }

                    for (int i = 0; i < prefs.AllScripts.Count; i++)
                    {
                        if (prefs[i].Enabled)
                        {
                            bool shouldRun = true;
                            if (prefs[i].Include.Length > 0)
                            {
                                shouldRun = false; // Default to false
                                for (int f = 0; f < prefs[i].Include.Length; f++)
                                {
                                    if (URL.ToString().Contains(prefs[i].Include[f].Replace("*", ""))) // Remove wildcards - not supported yet
                                    {
                                        shouldRun = true;
                                        break;
                                    }
                                }
                            }

                            if (shouldRun)
                            {
                                StreamReader str = new StreamReader(scriptPath + prefs[i].Path);
                                string _c = str.ReadToEnd();
                                str.Close();
                                try
                                {
                                    window.execScript(_c);
                                }
                                catch (Exception) { }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\nStack: \r\n" + ex.StackTrace + "\r\nSouce: \r\n" + ex.Source, "Scriptmonkey");
                checkInstall(); // Error may be caused by invalid installtion. Verify files haven't been deleted.
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
            return System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "Scriptmonkey.dll";
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
            if (prefs == null)
                prefs = new db(this);

            // Only need to check for install once per run.
            if (!installChecked)
                checkInstall();

            this.site = site;

            if (site != null)
            {
                var serviceProv = (IServiceProvider)this.site;
                var guidIWebBrowserApp = Marshal.GenerateGuidForType(typeof(IWebBrowserApp));
                var guidIWebBrowser2 = Marshal.GenerateGuidForType(typeof(IWebBrowser2));
                IntPtr intPtr;
                serviceProv.QueryService(ref guidIWebBrowserApp, ref guidIWebBrowser2, out intPtr);

                browser = (IWebBrowser2)Marshal.GetObjectForIUnknown(intPtr);

                ((DWebBrowserEvents2_Event)browser).DocumentComplete +=
                    new DWebBrowserEvents2_DocumentCompleteEventHandler(this.Run);
            }
            else
            {
                // Disabled this handler - no point running if there is not a site displayed
                //((DWebBrowserEvents2_Event)browser).DocumentComplete -=
                //    new DWebBrowserEvents2_DocumentCompleteEventHandler(this.Run);
                browser = null;
            }
            return 0;
        }
        int IObjectWithSite.GetSite(ref Guid guid, out IntPtr ppvSite)
        {
            IntPtr punk = Marshal.GetIUnknownForObject(browser);
            int hr = Marshal.QueryInterface(punk, ref guid, out ppvSite);
            Marshal.Release(punk);
            return hr;
        }
        #endregion

        #region Implementation of IOleCommandTarget
        int IOleCommandTarget.QueryStatus(IntPtr pguidCmdGroup, uint cCmds, ref OLECMD prgCmds, IntPtr pCmdText)
        {
            return 0;
        }
        int IOleCommandTarget.Exec(IntPtr pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            try
            {
                if (!installChecked) // Prevent loading if not installed
                    return 0;
                
                prefs.LoadData();
                Options form = new Options(prefs);
                if (form.ShowDialog() != DialogResult.Cancel)
                {
                    prefs = form.prefs;
                    prefs.Save();
                    prefs.ReloadDataAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\nStack: \r\n" + ex.StackTrace, "Scriptmonkey");
            }

            return 0;
        }
        #endregion

        #region Registering with regasm
        public static string RegBHO = "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Browser Helper Objects";
        public static string RegCmd = "Software\\Microsoft\\Internet Explorer\\Extensions";

        [ComRegisterFunction]
        public static void RegisterBHO(Type type)
        {
            string guid = type.GUID.ToString("B");

            // BHO
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(RegBHO, true);
                if (registryKey == null)
                    registryKey = Registry.LocalMachine.CreateSubKey(RegBHO);
                RegistryKey key = registryKey.OpenSubKey(guid);
                if (key == null)
                    key = registryKey.CreateSubKey(guid);
                key.SetValue("NoExplorer", 1);
                registryKey.Close();
                key.Close();
            }

            // Command
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(RegCmd, true);
                if (registryKey == null)
                    registryKey = Registry.LocalMachine.CreateSubKey(RegCmd);
                RegistryKey key = registryKey.OpenSubKey(guid);
                if (key == null)
                    key = registryKey.CreateSubKey(guid);
                key.SetValue("ButtonText", "Manage Userscripts");
                key.SetValue("CLSID", "{1FBA04EE-3024-11d2-8F1F-0000F87ABD16}");
                key.SetValue("ClsidExtension", guid);
                key.SetValue("Icon", Scriptmonkey.AssemblyPath() + ",1");
                key.SetValue("HotIcon", Scriptmonkey.AssemblyPath() + ",1");
                key.SetValue("Default Visible", "Yes");
                key.SetValue("MenuText", "&Manage Userscripts");
                key.SetValue("ToolTip", "Manage ScriptMonkey Userscripts");
                //key.SetValue("KeyPath", "no");
                registryKey.Close();
                key.Close();
            }
        }

        [ComUnregisterFunction]
        public static void UnregisterBHO(Type type)
        {
            string guid = type.GUID.ToString("B");
            // BHO
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(RegBHO, true);
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