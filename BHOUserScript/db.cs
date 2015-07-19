using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using BHOUserScript;
using System.Windows.Forms;
using System.Reflection;

namespace BHOUserScript
{
    /// <summary>
    /// Settings manager.
    /// </summary>
    public class db
    {
        Scriptmonkey main;

        public db(Scriptmonkey scr)
        {
            // This should be a pointer
            main = scr;
        }

        public SettingsFile Settings;

        // Alias for ReloadData
        public void LoadData()
        {
            ReloadData();
        }

        public void ReloadDataAsync()
        {
            Task s = ReloadDataAsyncTask();
        }

        private Task ReloadDataAsyncTask()
        {
            return Task.Run(() => ReloadData());
        }

        public void ReloadData()
        {
            try
            {
                StreamReader str = new StreamReader(Scriptmonkey.settingsFile);
                string _data = str.ReadToEnd();
                str.Close();
                Settings = JsonConvert.DeserializeObject<SettingsFile>(_data);
            }
            catch (Exception ex)
            {
                ReadSettingsFailureFrm form = new ReadSettingsFailureFrm();
                form.errorTxt.Text = ex.Message;
                DialogResult res = form.ShowDialog();
                if (res == DialogResult.Yes) // Delete everything
                {
                    DirectoryInfo info = new DirectoryInfo(Scriptmonkey.installPath);
                    foreach (FileInfo file in info.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in info.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                    main.checkInstall(); // Reinstall
                }
                else if (res == DialogResult.No) // Delete everything but scripts folder
                {
                    File.Delete(Scriptmonkey.installedFile);
                    File.Delete(Scriptmonkey.settingsFile);
                    main.checkInstall();
                }
            }
        }

        public void Save()
        {
            string _data = JsonConvert.SerializeObject(Settings);

            StreamWriter stw = new StreamWriter(Scriptmonkey.settingsFile);
            stw.Write(_data);
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
            Version v = Scriptmonkey.CurrentVersion();

            if (Settings.BHOCreatedVersion == null) // Really old versions didn't have BHOCreatedVersion property
                Settings.BHOCreatedVersion = new Version();

            if(v != Settings.BHOCreatedVersion)
            {
                // Do upgrades here
                // ...

                // Finished upgrading settings file. Now to update stored version
                Settings.BHOCreatedVersion = v;
                Save();
            }
        }
    }
}
