using System;
using System.Threading;

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

        public void Verify(string currentUrl)
        {
            Scriptmonkey.SendWebRequest("http://localhost:" + Port + "/verify/" + _key + '/' + currentUrl.Replace('/', '§'), true);
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
            if (!String.IsNullOrEmpty(_key))
                return;

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
