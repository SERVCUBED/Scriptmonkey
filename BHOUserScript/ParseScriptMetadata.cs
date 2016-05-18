using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace BHOUserScript
{
    /// <summary>
    /// Parses scripts for meta values and returns them.
    /// </summary>
    static class ParseScriptMetadata
    {
        static readonly string Name = @"@name( |\t)+([a-zA-Z\d :.,/\*_\+\?!\-\(\)]+)";
        static readonly string Description = @"@description( |\t)+([a-zA-Z\d :.,/\*_\+\?!\-\(\)]+)";
        static readonly string Author = @"@author( |\t)+([a-zA-Z\d :.,/\*_\+\?!\-\(\)]+)";
        static readonly string Version = @"@version( |\t)+([a-zA-Z\d :.,/\*_\+\?!\-\(\)]+)";
        static readonly string Match = @"@match( |\t)+([a-zA-Z\d :.,/\*_\+\?!\-\(\)]+)";
        static readonly string Include = @"@include( |\t)+([a-zA-Z\d :.,/\*_\+\?!\-\(\)]+)";
        static readonly string Exclude = @"@exclude( |\t)+([a-zA-Z\d :.,/\*_\+\?!\-\(\)]+)";
        static readonly string UpdateUrl = @"@updateURL( |\t)+([a-zA-Z\d :.,/\*_\+\?!\-\(\)]+)";
        static readonly string DownloadUrl = @"@downloadURL( |\t)+([a-zA-Z\d :.,/\*_\+\?!\-\(\)]+)";
        static readonly string Resource = @"@resource( |\t)+([a-zA-Z\d :.,/\*_\+\?!\-\(\)]+)( |\t)+([a-zA-Z\d :.,/\*_\+\?!\-\(\)]+)";

        public static Script Parse(string path)
        {
            StreamReader str = new StreamReader(Scriptmonkey.ScriptPath + path);
            string contents = str.ReadToEnd();
            str.Close();
            return ParseFromContents(contents);
        }

        public static Script ParseFromContents(string contents)
        {
            Script scr = new Script();
            try
            {
                scr.Name = GetContents(contents, Name);

                scr.Description = GetContents(contents, Description);

                scr.Author = GetContents(contents, Author);

                scr.Version = GetContents(contents, Version);

                Regex reg = new Regex(Match);
                Regex reg2 = new Regex(Include);
                MatchCollection matches = reg.Matches(contents);
                MatchCollection matches2 = reg2.Matches(contents);
                // Merge into single list
                List<Match> matches3 = new List<Match>();
                for (var i = 0; i < matches.Count; i++)
                {
                    matches3.Add(matches[i]);
                }
                for (var i = 0; i < matches2.Count; i++)
                {
                    matches3.Add(matches2[i]);
                }
                scr.Include = new string[matches3.Count];
                for (int i = 0; i < matches3.Count; i++)
                {
                    scr.Include[i] = matches3[i].Groups[2].Value;
                }

                reg = new Regex(Exclude);
                matches = reg.Matches(contents);
                scr.Exclude = new string[matches.Count];
                for (int i = 0; i < matches.Count; i++)
                {
                    scr.Exclude[i] = matches[i].Groups[2].Value;
                }

                scr = ParseResources(scr, contents);

                scr.UpdateUrl = GetContents(contents, DownloadUrl);

                if (scr.UpdateUrl == String.Empty) scr.UpdateUrl = GetContents(contents, UpdateUrl);

                scr.SavedValues = new Dictionary<string, string>();

                scr.Enabled = true;

                return scr;
            }
            catch (Exception ex)
            {
                if (Scriptmonkey.LogAndCheckDebugger(ex, "Error parsing script metadata"))
                    throw;
            }
            return scr;
        }

        public static Script ParseResources(Script s, string contents)
        {
            var reg = new Regex(Resource);
            var matches = reg.Matches(contents);
            s.Resources = new Dictionary<string, string>();
            foreach (Match match in matches)
            {
                s.Resources.Add(match.Groups[2].Value, match.Groups[3].Value);
            }
            return s;
        }

        private static string GetContents(string c, string regex)
        {
            var r = new Regex(regex);
            var m = r.Match(c);
            if (m == null)
                return "";
            else
                return m.Groups[2].Value;
        }
    }
}
