using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using System.EnterpriseServices.Internal;

namespace InstallationManager
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

            Console.Title = "Scriptmonkey Installation Manager";

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
                        if (version != null)
                        {
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
                File.Create(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\.CanIRunItTemp.temp").Dispose();

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
                AskInstall();
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
            Console.WriteLine("\r\n>> All done! Press any key to exit.");
            Console.ReadKey();

            // Go back go original colour configuration
            Console.ForegroundColor = defaultFore;
            Console.BackgroundColor = defaultBack;
        }

        static void AskInstall()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\r\nInstall Scriptmonkey? Press the key:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("'1' Install Scriptmonkey\r\n'2' Remove Scriptmonkey\r\n'3' Exit");
            var key = Console.ReadKey().Key;
            Console.ForegroundColor = ConsoleColor.Gray;
            if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1)
                Install();
            else if (key == ConsoleKey.D2 || key == ConsoleKey.NumPad2)
                Remove();
            else if (key == ConsoleKey.D3 || key == ConsoleKey.NumPad3)
                return;
            else
            {
                // Try again
                Console.WriteLine("\r\nUnknown key, try again.");
                AskInstall();
            }
        }

        // File paths
        static string driveLetter = Path.GetPathRoot(Environment.SystemDirectory);
        static string regAsm32Path = driveLetter + "Windows\\Microsoft.NET\\Framework\\v4.0.30319\\RegAsm.exe";
        static string regAsm64Path = driveLetter + "Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\RegAsm.exe";
        static string mainDllPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Scriptmonkey.dll";
        static string jsonDllPath = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Newtonsoft.Json.dll";

        static void Install() {
            // Make sure file exists
            Console.WriteLine("\r\n> Checking files...");
            if (!File.Exists(mainDllPath) || !File.Exists(jsonDllPath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\r\n>> Unable to find all the required files. Please extract all files from the archive then try again.");
                return;
            }
            Console.WriteLine("File check success!");

            // Is 64 bit .NET installed?
            bool is64BitInstalled = File.Exists(regAsm64Path);
            if (is64BitInstalled) Console.WriteLine("64-Bit check success");

            // Publish to GAC
            var pub = new Publish();
            pub.GacInstall(mainDllPath);
            pub.GacInstall(jsonDllPath);

            // Register with RegAsm
            RunProcess(regAsm32Path, mainDllPath);
            if (is64BitInstalled)
                RunProcess(regAsm64Path, mainDllPath);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\r\n>> Successfully installed Scriptmonkey!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Make sure you enable the addon when you start Internet Explorer (bar at the bottom).");
        }

        static void Remove()
        {
            // Make sure file exists
            Console.WriteLine("\r\n> Checking files...");
            if (!File.Exists(mainDllPath) || !File.Exists(jsonDllPath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\r\n>> Unable to find all the required files. Please extract all files from the archive then try again.");
                return;
            }
            Console.WriteLine("File check success!");

            // Is 64 bit .NET installed?
            bool is64BitInstalled = File.Exists(regAsm64Path);
            if (is64BitInstalled) Console.WriteLine("64-Bit check success");

            // Unregister with RegAsm
            RunProcess(regAsm32Path, "/unregister " + mainDllPath);
            if (is64BitInstalled)
                RunProcess(regAsm64Path, "/unregister " + mainDllPath);

            // Remove from GAC
            var pub = new Publish();
            pub.GacRemove(mainDllPath);
            pub.GacRemove(jsonDllPath);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\r\n>> Successfully removed Scriptmonkey!");
        }

        static void RunProcess(string path, string args)
        {
            var p = new Process();
            p.StartInfo.FileName = path;
            p.StartInfo.Arguments = args;
            try
            {
                p.Start();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unable to start " + path);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
