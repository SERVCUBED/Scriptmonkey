using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BHOUserScript
{
    /// <summary>
    /// Parses scripts for meta values and returns them.
    /// </summary>
    internal static class ParseScriptMetadata
    {
        private const string Value = @"( |\t)+([a-zA-Z\d :.,/\*_\+\?!\-\(\)\[\]]+)";
        private const string Name = @"@name" + Value;
        private const string Description = @"@description" + Value;
        private const string Author = @"@author" + Value;
        private const string Version = @"@version" + Value;
        private const string Match = @"@match" + Value;
        private const string Include = @"@include" + Value;
        private const string Exclude = @"@exclude" + Value;
        private const string Require = @"@require" + Value;
        private const string UpdateUrl = @"@updateURL" + Value;
        private const string DownloadUrl = @"@downloadURL" + Value;
        private const string Resource = @"@resource" + Value + Value;
        private const string InstallDisabled = @"@install-disabled";

        public static Script Parse(string path, bool isCss, Script s)
        {
            StreamReader str = new StreamReader(Scriptmonkey.ScriptPath + path);
            string contents = str.ReadToEnd();
            str.Close();
            return ParseFromContents(contents, isCss, s);
        }

        public static Script ParseFromContents(string contents, bool isCss, Script scr)
        {
            try
            {
                scr.Name = GetContents(contents, Name, scr.Name);

                scr.Description = GetContents(contents, Description, scr.Description);

                scr.Author = GetContents(contents, Author, scr.Author);

                if (!isCss)
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
                // If script has matches, use the parsed values. Otherwise, preserve current matches
                if (scr.Include.Length > 0 && matches3.Count > 0)
                {
                    scr.Include = new string[matches3.Count];
                    for (int i = 0; i < matches3.Count; i++)
                    {
                        scr.Include[i] = matches3[i].Groups[2].Value;
                    }
                }

                reg = new Regex(Exclude);
                matches = reg.Matches(contents);
                if (scr.Exclude.Length > 0 && matches.Count > 0)
                {
                    scr.Exclude = new string[matches.Count];
                    for (int i = 0; i < matches.Count; i++)
                    {
                        scr.Exclude[i] = matches[i].Groups[2].Value;
                    }
                }

                if (!isCss)
                {
                    scr = UpdateParsedData(scr, contents, isCss);

                    scr.UpdateUrl = GetContents(contents, DownloadUrl);

                    if (scr.UpdateUrl == String.Empty) scr.UpdateUrl = GetContents(contents, UpdateUrl);
                }

                scr.SavedValues = new Dictionary<string, string>();

                scr.Enabled = !contents.Contains(InstallDisabled);

                return scr;
            }
            catch (Exception ex)
            {
                if (Scriptmonkey.LogAndCheckDebugger(ex, "Error parsing script metadata"))
                    throw;
            }
            return scr;
        }

        /// <summary>
        /// Parse the metadata block for resources and dependencies if it not CSS.
        /// </summary>
        /// <param name="s">The script object.</param>
        /// <param name="contents">The contents of the script.</param>
        /// <param name="isCss">If the script is CSS.</param>
        /// <returns>The populated script object.</returns>
        public static Script UpdateParsedData(Script s, string contents, bool isCss)
        {
            // CSS can't have resources and dependencies.
            if (isCss)
                return s;

            //Requires API (should the API wrapper be added to the script)
            s.RequiresApi = contents.Contains("GM_");

            // Resources
            var reg = new Regex(Resource);
            var matches = reg.Matches(contents);
            s.Resources = new Dictionary<string, string>();
            foreach (Match match in matches)
            {
                s.Resources.Add(match.Groups[2].Value, match.Groups[3].Value);
            }

            // Require
            // Get new values
            if (s.Require == null)
                s.Require = new Dictionary<string, string>();

            reg = new Regex(Require);
            matches = reg.Matches(contents);
            var req = new List<string>();
            for (int i = 0; i < matches.Count; i++)
            {
                req.Add(matches[i].Groups[2].Value);
            }
            // Remove old values
            for (int i = 0; i < s.Require.Count; i++)
            {
                var key = s.Require.Keys.ToArray()[i];
                if (!req.Contains(key))
                {
                    s.Require.Remove(key);
                    i--;
                }
            }
            // Add values not in list
            foreach (string t in req.Where(t => !s.Require.ContainsKey(t)))
            {
                s.Require.Add(t, Scriptmonkey.SendWebRequest(t));
            }

            return s;
        }

        private static string GetContents(string c, string regex, string defaultValue = "")
        {
            var r = new Regex(regex);
            var m = r.Match(c);
            if (m == null)
                return defaultValue;
            else
            {
                var v = m.Groups[2].Value;
                return String.IsNullOrEmpty(v) ? defaultValue : v;
            }
        }
    }
}
