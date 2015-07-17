using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BHOUserScript
{
    /// <summary>
    /// Parses scripts for meta values and returns them.
    /// </summary>
    class ParseScriptMetadata
    {
        static string name = @"// +@name( |\t)+([a-zA-Z\d :.,/\*_-]+)";
        static string description = @"// +@description( |\t)+([a-zA-Z\d :.,/\*_-]+)";
        static string author = @"// +@author( |\t)+([a-zA-Z\d :.,/\*_-]+)";
        static string version = @"// +@version( |\t)+([a-zA-Z\d :.,/\*_-]+)";
        static string match = @"// +@match( |\t)+([a-zA-Z\d :.,/\*_-]+)";
        static string include = @"// +@include( |\t)+([a-zA-Z\d :.,/\*_-]+)";
        static string updateURL = @"// +@updateURL( |\t)+([a-zA-Z\d :.,/\*_-]+)";
        
        public static Script Parse(string path)
        {
            StreamReader str = new StreamReader(Scriptmonkey.scriptPath + path);
            string contents = str.ReadToEnd();
            str.Close();

            Script scr = new Script();
            scr.Name = GetContents(contents, name);

            scr.Description = GetContents(contents, description);

            scr.Author = GetContents(contents, author);

            scr.Version = GetContents(contents, version);

            Regex reg = new Regex(match);
            Regex reg2 = new Regex(include);
            MatchCollection _matches = reg.Matches(contents);
            MatchCollection _matches2 = reg2.Matches(contents);
            // Merge into single list
            List<Match> _matches3 = new List<Match>();
            for (int i = 0; i < _matches.Count; i++)
            {
                if (_matches[i] != null)
                    _matches3.Add(_matches[i]);
            }
            for (int i = 0; i < _matches2.Count; i++)
            {
                if (_matches2[i] != null)
                    _matches3.Add(_matches2[i]);
            }
            scr.Include = new string[_matches3.Count];
            for (int i = 0; i < _matches3.Count; i++)
            {
                scr.Include[i] = _matches3[i].Groups[2].Value;
            }
            scr.UpdateURL = GetContents(contents, updateURL);

            scr.Enabled = true;
            return scr;
        }

        private static string GetContents(string c, string regex)
        {
            Regex r = new Regex(regex);
            Match m = r.Match(c);
            if (m == null)
                return "";
            else
                return m.Groups[2].Value;
        }
    }
}
