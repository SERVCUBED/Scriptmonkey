using System;

namespace BHOUserScript
{
    class UpdateResponse
    {
        public bool Success;
        public string Error;
        public Version LatestVersion;
        public string ChangeList;

        public string Changes
        {
            get{
                return ChangeList.Replace("&LBreak;", Environment.NewLine);
            }
        }
    }
}
