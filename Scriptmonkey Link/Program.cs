using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scriptmonkey_Link
{
    static class Program
    {
        private static readonly Mutex _mutex = new Mutex(true, "{fdabff7f-d5d9-4799-aa5c-aa6ea403968b}");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (_mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                _mutex.ReleaseMutex();
            }
        }
    }
}
