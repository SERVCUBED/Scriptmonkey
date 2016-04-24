using System;
using System.Collections.Generic;

namespace Scriptmonkey_Link
{
    class InstanceData
    {
        public DateTime LastRequestTime = DateTime.Now;

        public readonly List<string> Queue = new List<string>(); 
    }
}
