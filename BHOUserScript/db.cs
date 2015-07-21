using System;
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
        readonly Scriptmonkey _main;

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
            try
            {
                StreamReader str = new StreamReader(Scriptmonkey.SettingsFile);
                string data = str.ReadToEnd();
                str.Close();
                Settings = JsonConvert.DeserializeObject<SettingsFile>(data);
            }
            catch (Exception ex)
            {
                var form = new ReadSettingsFailureFrm {errorTxt = {Text = ex.Message}};
                var res = form.ShowDialog();
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
                    _main.CheckInstall();
                }
            }
        }

        public void Save()
        {
            var data = JsonConvert.SerializeObject(Settings);

            var stw = new StreamWriter(Scriptmonkey.SettingsFile);
            stw.Write(data);
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

                // Finished upgrading settings file. Now to update stored version
                Settings.BhoCreatedVersion = v;
                Save();
            }
        }
    }
}
