using System;
using System.Collections.Generic;
using System.Threading;
using mshtml;

namespace BHOUserScript
{
    class ScriptmonkeyLinkManager : IDisposable
    {
        private readonly string _url;
        private string _key;
        private bool _ticking;

        public delegate void OnReceiveEventHandler(string action);
        public event OnReceiveEventHandler OnReceiveEvent;

        public ScriptmonkeyLinkManager(string url)
        {
            _url = url;
            ThreadPool.QueueUserWorkItem(o => RegisterNew());
        }

        public void SendAction(string action)
        {
            Scriptmonkey.SendWebRequest(_url + "action/" + _key + '/' + action, true);
        }

        public void Verify(string currentUrl)
        {
            Scriptmonkey.SendWebRequest(_url + "verify/" + _key + '/' + currentUrl.Replace('/', '§'), true);
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
                    Thread.Sleep(10000);

                    if (String.IsNullOrEmpty(_key))
                        return;

                    Tick();
                }
            });
        }

        private void Tick()
        {

            string data = Scriptmonkey.SendWebRequest(_url + "req/" + _key);

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
            if (!String.IsNullOrEmpty(_key))
                return;

            string data =
                    Scriptmonkey.SendWebRequest(_url + "register", true);
            if (data == "errinvalidgenkey")
                RegisterNew();
            else if (!String.IsNullOrEmpty(data))
                _key = data;
        }

        public void SendNotify(string title, string text, string currentUrl)
        {
            Scriptmonkey.SendWebRequest(_url + "notify/" + _key, true, title + '&' + text + '&' + currentUrl);
        }

        public void Dispose()
        {
            _key = null;
        }
    }
}
