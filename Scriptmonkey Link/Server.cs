using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

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

        public delegate void OnReceivedEvent(string key, string data);

        public event OnReceivedEvent OnReceived;

        public Server()
        {
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
                        var url = split[1];
                        OnReceived?.Invoke(key, url.Replace('§', '/'));
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
            DateTime tPurge = DateTime.Now.Subtract(TimeSpan.FromMinutes(30));
            for (int i = 0; i < _instances.Count; i++)
            {
                if (_instances.ElementAt(i).Value.LastRequestTime < tPurge)
                {
                    _instances.Remove(_instances.ElementAt(i).Key);
                    OnReceived?.Invoke(_instances.ElementAt(i).Key, "purged");
                }
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

        #region From Scriptmonkey

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
        /// Generate a random number to use as a script prefix and avoid duplicate installs
        /// </summary>
        /// <returns>A random number from 0 to 100,000</returns>
        public static string GenerateRandomString()
        {
            Random r = new Random();
            return (char)(r.Next(97, 122)) + Convert.ToString(r.Next(100000));
        }

        #endregion
    }
}
