using UnityEngine;

namespace SamplePlugin
{
    internal class ModHooks
    {
        public static void ModLog(string message)
        {
            Debug.Log(message);
        }
    }
}
