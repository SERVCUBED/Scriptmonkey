using System;

namespace BHOUserScript
{
    /// <summary>
    /// Contains all properties for each installed script.
    /// </summary>
    public class Script
    {
        public string Name {get;set;}

        public string Path { get; set; }

        public string Author { get; set; }

        public string Version { get; set; }

        // Array for require properties
        public string[] Require { get; set; }

        public string Description { get; set; }

        public string UpdateURL { get; set; }

        // URLs for the script to be run on
        public string[] Include { get; set; }

        // Needed when checking if BHO has been updated after script data has been saved due to possibility of changes to storage system
        public Version LastUsedBHOVersion { get; set; }

        // Will be used when checking for updates after ... days after installation
        public DateTime InstallDate { get; set; }

        public bool Enabled { get; set; }
    }
}
