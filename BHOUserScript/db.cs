﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace BHOUserScript
{
    /// <summary>
    /// Settings manager.
    /// </summary>
    public class Db
    {
        private readonly Scriptmonkey _main;

        public Db(Scriptmonkey scr)
        {
            // This should be a pointer
            _main = scr;
        }

        public SettingsFile Settings;

        // Alias for ReloadData
        public void LoadData()
        {
            ReloadData();
        }

        public void ReloadDataAsync()
        {
            var s = ReloadDataAsyncTask();
        }

        private Task ReloadDataAsyncTask()
        {
            return Task.Run(() => ReloadData());
        }

        public void ReloadData()
        {
            //StreamReader str = new StreamReader(Scriptmonkey.SettingsFile);
            try
            {
                //string data = str.ReadToEnd();
                var data = ReadFile(Scriptmonkey.SettingsFile);
                Settings = JsonConvert.DeserializeObject<SettingsFile>(data);
            }
            catch (Exception ex)
            {
                Reset(ex);
            }
            //str.Close();
        }

        public void Reset(Exception ex)
        {
            Scriptmonkey.Log(ex, "\r\nReset?");
            var form = new ReadSettingsFailureFrm { errorTxt = { Text = ex.Message } };
            var res = form.ShowDialog();
            form.Dispose();
            if (res == DialogResult.Yes) // Delete everything
            {
                DirectoryInfo info = new DirectoryInfo(Scriptmonkey.InstallPath);
                foreach (var file in info.GetFiles())
                {
                    file.Delete();
                }
                foreach (var dir in info.GetDirectories())
                {
                    dir.Delete(true);
                }
                _main.CheckInstall(); // Reinstall
            }
            else if (res == DialogResult.No) // Delete everything but scripts folder
            {
                File.Delete(Scriptmonkey.InstalledFile);
                File.Delete(Scriptmonkey.SettingsFile);
                DirectoryInfo resourceDir = new DirectoryInfo(Scriptmonkey.ResourcePath);
                foreach (var file in resourceDir.GetFiles())
                {
                    file.Delete();
                }
                Directory.Delete(Scriptmonkey.ResourcePath);
                _main.CheckInstall();
            }
        }

        public void Save()
        {
            var stw = new StreamWriter(Scriptmonkey.SettingsFile);
            try
            {
                var data = JsonConvert.SerializeObject(Settings);
                stw.Write(data);
            }
            catch (Exception ex)
            {
                Scriptmonkey.Log(ex, "Unable to save");
            }
            stw.Close();
        }

        public void AddScript(Script add)
        {
            Settings.Scripts.Add(add);
            Save();
        }

        // Use this instead of AllScripts[index] when getting individual script for error handling reasons
        public Script this[int index]
        {
            get
            {
                if (Settings.Scripts.Count - 1 >= index) // Must -1 as index is base 0
                    return Settings.Scripts[index];
                return null;
            }
            set { 
                Settings.Scripts[index] = value;
                Save();
            }
        }

        public List<Script> AllScripts
        {
            get
            {
                return Settings.Scripts;
            }

            set
            {
                Settings.Scripts = value;
            }
        }

        public void CheckUpdateStatus()
        {
            // Get assembly version
            var v = Scriptmonkey.CurrentVersion();

            if (Settings.BhoCreatedVersion == null) // Really old versions didn't have BHOCreatedVersion property
                Settings.BhoCreatedVersion = new Version();

            if(v != Settings.BhoCreatedVersion)
            {
                // Do upgrades here
                // ...

                if (Settings.LastUpdateCheckDate == null)
                    Settings.LastUpdateCheckDate = DateTime.Now;

                foreach (Script s in AllScripts)
                {
                    if (s.Exclude == null)
                        s.Exclude = new string[0];

                    if (s.SavedValues == null)
                        s.SavedValues = new Dictionary<string, string>();

                    if (s.Resources == null)
                        s.Resources = new Dictionary<string, string>();

                    if (!Directory.Exists(Scriptmonkey.ResourcePath))
                        Directory.CreateDirectory(Scriptmonkey.ResourcePath);
                }

                // Finished upgrading settings file. Now to update stored version
                Settings.BhoCreatedVersion = v;
                Save();
            }
        }

        private static string ReadFile(string url)
        {
            using (var str = new FileStream(url, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                const int maxFileSize = 20 * 1024 * 1024;
                var fileBytes = new byte[maxFileSize];

                var numBytes = str.Read(fileBytes, 0, maxFileSize);

                var utf7 = new System.Text.UTF7Encoding();

                return utf7.GetString(fileBytes, 0, numBytes);
            }
        }
    }
}
