using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHDocVw;

namespace Scriptmonkey_Link
{
    internal static class IETools
    {
        public delegate void IEOperation(InternetExplorer iExplorer);

        /// <summary>
        /// Performs an operation on each instance of Internet Explorer
        /// </summary>
        /// <param name="operation"></param>
        public static void TryForEachInternetExplorer(IEOperation operation, bool justOnce = false)
        {
            ShellWindows iExplorerInstances = new ShellWindows();
            foreach (var iExplorerInstance in iExplorerInstances)
            {
                try
                {
                    var iExplorer = (InternetExplorer)iExplorerInstance;
                    if (iExplorer.Name == "Internet Explorer" || iExplorer.Name == "Windows Internet Explorer")
                    {
                        operation(iExplorer);
                        if (justOnce)
                            break;
                    }
                }
                catch (Exception) { }
            }
        }
    }
}
