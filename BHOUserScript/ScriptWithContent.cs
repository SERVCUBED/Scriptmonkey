using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHOUserScript
{
    class ScriptWithContent
    {
        public Script ScriptData { get; set; }

        public string Content { get; set; }

        public ScriptWithContent(Script s)
        {
            ScriptData = s;
        }
    }
}
