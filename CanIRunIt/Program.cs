using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.IO;

namespace CanIRunIt
{
    static class Program
    {
        const double NETversion = 4.5;

        /// <summary>
        /// The main entry point for the application.
        /// 
        /// This program checks if the user's machine can run Scriptmonkey by checking if it has the 
        /// correct versions of .NET Framework installed and has write access to the userprofile 
        /// directory for script storage.
        /// 
        /// This is compiled using .NET Framework v3.0 as that version is included in all recent Windows
        /// versions (Vista onwards - see https://en.wikipedia.org/wiki/.NET_Framework#Versions for more info).
        /// </summary>
        static void Main()
        {
            // User might use custom colours. Save these to revert to on exit.
            ConsoleColor defaultFore = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleColor defaultBack = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Black;

            Console.Title = "Can I Run Scriptmonkey?";

            Console.WriteLine("Can I Run Scriptmonkey?\r\n");

            // Registry
            Console.WriteLine("> Testing registry...");

            bool _registry = false;
            try
            {
                RegistryKey installed_versions = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP");
                string[] version_names = installed_versions.GetSubKeyNames();
                for (int i = 0; i < version_names.Length; i++)
                {
                    try
                    {
                        RegistryKey version = installed_versions.OpenSubKey(version_names[i]);
                        RegistryKey client = version.OpenSubKey(version.GetSubKeyNames()[0]);
                        string v = client.GetValue("Version").ToString();
                        if (v != null) // If 'Version' key exists
                        {
                            double v_d = Convert.ToDouble(v.Substring(0, 3)); // Only the 1st decimal point needed
                            if (v_d >= NETversion)
                            {
                                _registry = true;
                                break;
                            }
                        }
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception) { }
            if (_registry)
                Console.WriteLine("Registry success");
            else
                Console.WriteLine("Registry fail");

            // File path
            Console.WriteLine("> Checking file paths...");

            bool _file = false;
            try
            {
                if (Directory.Exists("C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\"))
                    _file = true;
                if (Directory.Exists("C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\"))
                    _file = true;
            }
            catch (Exception) { }

            if (_file)
                Console.WriteLine("File paths success");
            else
                Console.WriteLine("File paths fail");

            // Script path access
            Console.WriteLine("> User directory access...");

            bool _user = false;
            try
            {
                // Try creating a file in the appdata folder - SpecialFolder.UserProfile not available in .NET 3.0
                File.Create(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\.CanIRunItTemp.temp");

                _user = true;
            }
            catch (Exception) { }

            if (_user)
                Console.WriteLine("User directory success");
            else
                Console.WriteLine("User directory fail");

            if (_registry && _file && _user)
            {
                Console.ForegroundColor = ConsoleColor.Green; // Different colours to make text easier for the user to read. 
                Console.WriteLine("\r\n>> Yes! You can run Scriptmonkey!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\r\n>> Oops! Looks like you can't run Scriptmonkey.\r\n");
                Console.ForegroundColor = ConsoleColor.White;
                if (!_registry)
                    Console.WriteLine("> You should install the latest version of the .NET Framework (4.5 or later) and try again.");
                if (!_file && _registry)
                    Console.WriteLine("> You should (re)install the .NET Framework version 4 or greater to avoid having to manually register the files.");
                if (!_user)
                    Console.WriteLine("> You don't have write access to your user folder. Get write permissions from your system administrator and try again.");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\r\nDo the above then run this tool again.");
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\r\nAll done! Press any key to exit.");
            Console.ReadKey();

            // Go back go original colour configuration
            Console.ForegroundColor = defaultFore;
            Console.BackgroundColor = defaultBack;
        }
    }
}
