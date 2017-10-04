using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DebugMod
{
    class ModHooks
    {
        public static void ModLog(string message)
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter("@E:\\DEBUG.txt", true))
            {
                writer.WriteLine(message);
            }
        }
    }
}
