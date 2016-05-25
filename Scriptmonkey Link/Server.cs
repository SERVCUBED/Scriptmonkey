using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.IO.Compression;
using SHDocVw;

namespace Scriptmonkey_Link
{
    /// <summary>
    /// A server for Scriptmonkey Link.
    /// 
    /// API is as follows:
    ///    REQUEST:                RETURNS:                            INFO:
    ///     /ping                   pong/[Version Information]          Used to check if the server is up
    ///     /about                  [Friendly Version Information]      Displays friendly version information
    ///     /register               [key]                               Registers the client and returns the key to access the action queue
    ///     /unregister/[key]                                           Deletes the key and action queue from the server
    ///     /req/[key]              [Action]                            Returns the next action in the queue for the specified key
    ///     /action/[key]/[Action]  success                             Adds the action to the queue of all the keys except the one specified
    /// </summary>
    class Server : IDisposable
    {
        readonly HttpListener _listener = new HttpListener();
        readonly Dictionary<string, InstanceData> _instances = new Dictionary<string, InstanceData>();
        private const int Port = 32888;
        private string _savedWindowsPath = String.Empty;

        public delegate void OnReceivedEvent(string key, string data);

        public event OnReceivedEvent OnReceived;

        public Server()
        {
            _savedWindowsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                Path.DirectorySeparatorChar + ".ScriptmonkeyLinkSavedWindows.tmp";

            _listener.Prefixes.Add("http://localhost:" + Port + "/");
            _listener.Prefixes.Add("http://127.0.0.1:" + Port + "/");

            TryStartListen();
        }

        /// <summary>
        /// Make incoming requests be handled by ListenAsync
        /// </summary>
        private void Listen()
        {
            while (true)
            {
                try
                {
                    if (!_listener.IsListening)
                        return;
                    var context = _listener.GetContext();
                    ThreadPool.QueueUserWorkItem(o => ListenAsync(context));
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// Safely stop the listener
        /// </summary>
        public void Dispose()
        {
            if (_listener.IsListening)
                _listener.Stop();
        }

        /// <summary>
        /// Handle web requests
        /// </summary>
        /// <param name="state">The HttpListenerContext</param>
        private void ListenAsync(object state)
        {
            try
            {
                var context = (HttpListenerContext)state;

                context.Response.StatusCode = 200;
                context.Response.SendChunked = true;

                string content = String.Empty;

                if (context.Request.Url.AbsolutePath == "/ping")
                {
                        var v = CurrentVersion();
                        content = "pong/ScriptmonkeyLink/" + v.Major + "." + v.Minor + "." + v.Revision;
                }
                else if (context.Request.Url.AbsolutePath == "/about")
                {
                        var v = CurrentVersion();
                        content = "ScriptmonkeyLink HTTP Service v" + v.Major + "." + v.Minor;
                }
                else if (context.Request.Url.AbsolutePath == "/register")
                {
                    var genKey = GenerateRandomString();
                    if (!_instances.ContainsKey(genKey))
                    {
                        _instances.Add(genKey, new InstanceData());
                        content = genKey;
                        OnReceived?.Invoke(genKey, "registered");
                    }
                    else
                    {
                        content = "errinvalidgenkey";
                    }
                }
                else if (context.Request.Url.AbsolutePath.StartsWith("/unregister/"))
                {
                    var key = context.Request.Url.AbsolutePath.Substring(12);
                    if (_instances.ContainsKey(key))
                    {
                        _instances.Remove(key);
                    }
                    else
                    {
                        content = "errinvalidkey";
                    }
                }
                else if (context.Request.Url.AbsolutePath.StartsWith("/req/"))
                {
                    var key = context.Request.Url.AbsolutePath.Substring(5);
                    if (_instances.ContainsKey(key))
                    {
                        if (_instances[key].Queue.Count > 0)
                        {
                            content = _instances[key].Queue[0];
                            _instances[key].Queue.RemoveAt(0);
                        }
                        else
                            content = "noaction";
                        _instances[key].LastRequestTime = DateTime.Now;
                    }
                    else
                    {
                        content = "errinvalidkey";
                    }
                }
                else if (context.Request.Url.AbsolutePath.StartsWith("/action/"))
                {
                    var split = context.Request.Url.AbsolutePath.Substring(8).Split('/');
                    if (split.Length == 2)
                    {
                        var key = split[0];
                        var action = split[1];
                        foreach (var i in _instances)
                        {
                            if (i.Key != key && !i.Value.Queue.Contains(action))
                                i.Value.Queue.Add(action);
                        }
                        content = "success";
                        OnReceived?.Invoke(key, action);
                    }
                    else
                        content = "errinvrequest";
                }
                else if (context.Request.Url.AbsolutePath.StartsWith("/verify/"))
                {
                    var split = context.Request.Url.AbsolutePath.Substring(8).Split('/');
                    if (split.Length == 2)
                    {
                        var key = split[0];
                        var url = Uri.UnescapeDataString(split[1]).Replace('§', '/');
                        
                        OnReceived?.Invoke(key, url);
                    }
                    else
                        content = "errinvrequest";
                }
                else
                {
                    content = "err404";
                    context.Response.StatusCode = 404;
                    context.Response.StatusDescription = "Invalid request: err404";
                }

                var bytes = Encoding.UTF8.GetBytes(content);
                context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                context.Response.OutputStream.Close();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Remove instances which have been inactive for 30 minutes
        /// </summary>
        public void Purge()
        {
            DateTime tPurge = DateTime.Now.Subtract(TimeSpan.FromMinutes(5));
            for (int i = 0; i < _instances.Count; i++)
            {
                if (i >= _instances.Count)
                    break;
                if (_instances.ElementAt(i).Value.LastRequestTime >= tPurge) continue;

                OnReceived?.Invoke(_instances.ElementAt(i).Key, "purged");
                _instances.Remove(_instances.ElementAt(i).Key);
                i--;
            }
        }

        /// <summary>
        /// Gets a value for if the server is listening
        /// </summary>
        public bool IsActive
        {
            get { return _listener.IsListening; }

            set
            {
                if (value && !_listener.IsListening)
                {
                    TryStartListen();
                    OnReceived?.Invoke("server", "start");
                }
                else if (!value && _listener.IsListening)
                {
                    _listener.Stop();
                    OnReceived?.Invoke("server", "stop");
                }
            }
        }

        /// <summary>
        /// Attempt to start listening on the port and queue the listener thread.
        /// </summary>
        private void TryStartListen()
        {
            try
            {
                _listener.Start();
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                if (ex is HttpListenerException)
                    m = "Unable to open port. Is it in use by another program?\r\nTry running as an administrator.\r\n\r\n" + m;

                MessageBox.Show(m);
                return;
            }

            ThreadPool.QueueUserWorkItem(o => Listen());
        }

        /// <summary>
        /// Send the action to all instances
        /// </summary>
        /// <param name="action">The action to broadcast</param>
        public void Broadcast(string action)
        {
            if (String.IsNullOrEmpty(action))
                return;
            foreach (var instance in _instances)
            {
                instance.Value.Queue.Add(action);
            }
        }

        /// <summary>
        /// The number of instances active since the last purge
        /// </summary>
        public int NumInstances => _instances.Count;

        public void DoBackup()
        {
            var profile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            var d = DateTime.Now;
            var saveUri = profile + $"\\Scriptmonkey.backup{d.Day}.{d.Month}.{d.Year}";

            var profilePath = profile + "\\.Scriptmonkey";
            if (Directory.Exists(profilePath))
            {
                performBackup(profilePath, saveUri);
                return;
            }

            var virtualised = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                "\\Microsoft\\Windows\\Temporary Internet Files\\Virtualized\\" + profilePath.Replace(":", "");

            if (Directory.Exists(virtualised))
            {
                performBackup(virtualised, saveUri);
                return;
            }

            MessageBox.Show(@"Settings file not found. You will need to backup manually.");
        }

        /// <summary>
        /// Backup a directory to the specified file.
        /// </summary>
        /// <param name="settingsPath">The directory to backup.</param>
        /// <param name="saveUri">The destination file path (without file extension).</param>
        private void performBackup(string settingsPath, string saveUri)
        {
            var file = saveUri;
            try
            {
                if (!Directory.Exists(settingsPath))
                    return;

                if (File.Exists(file + ".zip"))
                {
                    var exists = 1;
                    do
                    {
                        file = $"{saveUri} ({exists})";
                        exists++;
                    } while (File.Exists(file + ".zip"));
                }

                ZipFile.CreateFromDirectory(settingsPath, file + ".zip");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Whoops. Unable to backup the folder {settingsPath} to {file}.zip {Environment.NewLine + ex.Message}");
            }
        }

        public void StartSaveWindowState()
        {

            ThreadPool.QueueUserWorkItem((callback) =>
            {
                var s = String.Empty;

                TryForEachInternetExplorer((iExplorer) =>
                {
                    s += iExplorer.LocationURL + '\n';
                    iExplorer.Quit();
                });
                
                WriteFile(_savedWindowsPath, s);
            });
        }

        public void RestoreSavedWindows()
        {
            if (!File.Exists(_savedWindowsPath))
                return;

            var c = ReadFile(_savedWindowsPath);
            if (String.IsNullOrWhiteSpace(c))
                return;

            ThreadPool.QueueUserWorkItem((callback) =>
            {
                var s = c.Split('\n');
                if (s.Length == 0)
                    return;

                for (int index = 0; index < s.Length - 1; index++)
                {
                    var url = s[index];

                    OnReceived?.Invoke("restored", url);

                    // Open new tab, not a new window
                    var f = false;
                    TryForEachInternetExplorer((iExplorer) =>
                    {
                        iExplorer.Navigate2(url, BrowserNavConstants.navOpenInNewTab);
                        f = true;
                    });
                    if (!f) // No IE instances. Open new window
                    {
                        try
                        {
                            var p = new Process
                            {
                                StartInfo =
                                {
                                    FileName = "iexplore.exe",
                                    Arguments = url
                                }
                            };
                            p.Start();
                        }
                        catch (Exception)
                        {
                            OnReceived?.Invoke("iexplore", "Unable to start process");
                        }
                        Thread.Sleep(200); // Allow IE to start before opening new tabs
                    }
                }

                File.Delete(_savedWindowsPath);
            });
        }

        private delegate void IEOperation(InternetExplorer iExplorer);

        /// <summary>
        /// Performs an operation on each instance of Internet Explorer
        /// </summary>
        /// <param name="operation"></param>
        private void TryForEachInternetExplorer(IEOperation operation)
        {
            ShellWindows iExplorerInstances = new ShellWindows();
            foreach (var iExplorerInstance in iExplorerInstances)
            {
                try
                {
                    var iExplorer = (InternetExplorer) iExplorerInstance;
                    if (iExplorer.Name == "Internet Explorer" || iExplorer.Name == "Windows Internet Explorer")
                    {
                        operation(iExplorer);
                    }
                }
                catch (Exception)
                {
                    OnReceived?.Invoke("iexplore", "Unable to perform action");
                }
            }
        }

        public void RefreshAllInstances()
        {
            TryForEachInternetExplorer(i => i.Refresh2() );
        }

        #region From Scriptmonkey

        /// <summary>
        /// Gets the version of the current assembly.
        /// </summary>
        /// <returns>Assembly version</returns>
        private static Version CurrentVersion()
        {
            var thisApp = Assembly.GetExecutingAssembly();
            AssemblyName name = new AssemblyName(thisApp.FullName);
            return name.Version;
        }

        /// <summary>
        /// Generate a random number to use as a script prefix and avoid duplicate installs
        /// </summary>
        /// <returns>A random number from 0 to 100,000</returns>
        private static string GenerateRandomString()
        {
            Random r = new Random();
            return (char)(r.Next(97, 122)) + Convert.ToString(r.Next(100000));
        }

        /// <summary>
        /// Read a file without locking it
        /// </summary>
        /// <param name="url">The URL of the file to read.</param>
        /// <param name="onError">Optional. The string to return if the file cannot be found.</param>
        /// <returns></returns>
        private static string ReadFile(string url, string onError = null)
        {
            if (!File.Exists(url))
            {
                return onError;
            }

            int maxFileSize = 20 * 1024 * 1024;

            using (var str = new FileStream(url, FileMode.Open, FileAccess.Read, FileShare.Read))
            {

                var utf8 = new UTF8Encoding();

                byte[] fileBytes;
                int numBytes;

                // If the number of bytes to read equals the maximum file length, try to read more data from the file
                do
                {
                    fileBytes = new byte[maxFileSize];
                    numBytes = str.Read(fileBytes, 0, maxFileSize);

                    if (numBytes >= maxFileSize)
                        maxFileSize *= 2;

                } while (numBytes == maxFileSize);

                return utf8.GetString(fileBytes, 0, numBytes);
            }
        }

        private static void WriteFile(string url, string contents)
        {
            using (var str = new FileStream(url, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                var fileBytes = Encoding.ASCII.GetBytes(contents);

                str.Write(fileBytes, 0, fileBytes.Length);
            }
        }

        #endregion

        private enum BrowserNavConstants
        {
            navOpenInNewWindow = 1,
            navNoHistory = 2,
            navNoReadFromCache = 4,
            navNoWriteToCache = 8,
            navAllowAutosearch = 16,
            navBrowserBar = 32,
            navHyperlink = 64,
            navEnforceRestricted = 128,
            navNewWindowsManaged = 256,
            navUntrustedForDownload = 512,
            navTrustedForActiveX = 1024,
            navOpenInNewTab = 2048,
            navOpenInBackgroundTab = 4096,
            navKeepWordWheelText = 8192,
            navVirtualTab = 16384,
            navBlockRedirectsXDomain = 32768,
            navOpenNewForegroundTab = 65536
        }
    }
}
