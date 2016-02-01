using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
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
            Task.Run(() => ReloadData());
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
            Scriptmonkey.Log(ex, "Reset?");
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

        public void Save(bool immediate = false)
        {
            //var stw = new StreamWriter(Scriptmonkey.SettingsFile);
            try
            {
                var data = JsonConvert.SerializeObject(Settings);
                //stw.Write(data);
                if (immediate)
                    WriteFile(Scriptmonkey.SettingsFile, data);
                else
                    WriteFileAsync(Scriptmonkey.SettingsFile, data);
            }
            catch (Exception ex)
            {
                Scriptmonkey.Log(ex, "Unable to save");
            }
            //stw.Close();
        }

        public void AddScript(Script add)
        {
            Settings.Scripts.Add(add);
            Save(true);
        }

        // Use this instead of AllScripts[index] when getting individual script for error handling reasons
        public Script this[int index]
        {
            get
            {
                if (Settings.Scripts.Count - 1 >= index) // Must -1 as index is base 0
                    return Settings.Scripts[index];
                throw new IndexOutOfRangeException("Script index out of range");
            }
            set { 
                Settings.Scripts[index] = value;
                Save(true);
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

        private int _maxFileSize = 20 * 1024 * 1024;

        private string ReadFile(string url)
        {
            using (var str = new FileStream(url, FileMode.Open, FileAccess.Read, FileShare.Read))
            {

                var utf8 = new UTF8Encoding();

                byte[] fileBytes;
                int numBytes;

                // If the number of bytes to read equals the maximum file length, try to read more data from the file
                do
                {
                    fileBytes = new byte[_maxFileSize];
                    numBytes = str.Read(fileBytes, 0, _maxFileSize);
                    
                    if (numBytes >= _maxFileSize)
                        _maxFileSize *= 2;

                } while (numBytes == _maxFileSize);
                
                return utf8.GetString(fileBytes, 0, numBytes);
            }
        }

        private static void WriteFileAsync(string url, string contents)
        {
            var t = new Thread(() => WriteFile(url, contents));
            t.SetApartmentState(ApartmentState.MTA);
            t.Start();
        }

        private static void WriteFile(string url, string contents)
        {
            using (var str = new FileStream(url, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                var fileBytes = Encoding.ASCII.GetBytes(contents);

                str.Write(fileBytes, 0, fileBytes.Length);
            }
        }
    }
}
