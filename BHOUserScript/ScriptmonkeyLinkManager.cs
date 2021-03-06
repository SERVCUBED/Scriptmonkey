﻿using System;
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
        private int _delay;
        private readonly Version _minLinkVersion = new Version(1, 1);

        public delegate void OnReceiveEventHandler(string action);
        public event OnReceiveEventHandler OnReceiveEvent;

        public ScriptmonkeyLinkManager(string url, int delay)
        {
            _url = url;
            _delay = delay;
            ThreadPool.QueueUserWorkItem(o => RegisterNew());
        }

        public void SendAction(string action)
        {
            if (_key != String.Empty)
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
                    Thread.Sleep(_delay);

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
            var test = Scriptmonkey.SendWebRequest(_url + "ping", true);
            if (String.IsNullOrEmpty(test))
                return;
            var aTest = test.Split('/');
            if (aTest.Length != 3 || aTest[0] != "pong" || aTest[1] != "ScriptmonkeyLink")
                return;
            if (new Version(aTest[2]) < _minLinkVersion)
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
