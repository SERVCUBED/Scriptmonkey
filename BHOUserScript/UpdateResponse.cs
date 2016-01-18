using System;

namespace BHOUserScript
{
    class UpdateResponse
    {
        public bool Success = false;
        public string Error = String.Empty;
        public Version LatestVersion = null;
        public string ChangeList = String.Empty;

        public string Changes
        {
            get{
                return ChangeList.Replace("&LBreak;", Environment.NewLine);
            }
        }
    }
}
