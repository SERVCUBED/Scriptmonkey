using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SHDocVw;

namespace BHOUserScript
{
    class ScriptmonkeyLinkManager : IDisposable
    {
        private const int Port = 32888;
        private string _key;
        private bool _ticking;

        public delegate void OnReceiveEventHandler(string action);
        public event OnReceiveEventHandler OnReceiveEvent;

        public ScriptmonkeyLinkManager()
        {
            ThreadPool.QueueUserWorkItem(o => RegisterNew());
        }

        public void SendAction(string action)
        {
            Scriptmonkey.SendWebRequest("http://localhost:" + Port + "/action/" + _key + '/' + action, true);
        }

        public void TickAsync()
        {
            if (_ticking)
                return;
            _ticking = true;

            ThreadPool.QueueUserWorkItem(o =>
            {
                while (true)
                {
                    Thread.Sleep(5000);

                    if (String.IsNullOrEmpty(_key))
                        return;

                    Tick();
                }
            });
        }

        private void Tick()
        {

            string data = Scriptmonkey.SendWebRequest("http://localhost:" + Port + "/req/" + _key);

            if (data == "errinvalidkey")
            {
                // Get a new key
                RegisterNew();
            }
            else if (!String.IsNullOrEmpty(data))
                OnReceiveEvent?.Invoke(data);
        }

        private void RegisterNew()
        {
            string data =
                    Scriptmonkey.SendWebRequest("http://localhost:" + Port + "/register", true);
            if (data == "errinvalidgenkey")
                RegisterNew();
            else if (!String.IsNullOrEmpty(data))
                _key = data;
        }

        public void Dispose()
        {
            _key = null;
        }
    }
}
