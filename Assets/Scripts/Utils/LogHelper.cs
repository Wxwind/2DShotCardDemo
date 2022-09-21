using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Utils
{
    public static class LogHelper
    {
        [Conditional("Log")]
        public static void LogInfo(string msg)
        {
            Debug.Log(msg);
        }

        [Conditional("Log")]
        public static void LogError(string error)
        {
            Debug.LogError(error);
        }
        
        [Conditional("Log")]
        public static void LogWarning(string error)
        {
            Debug.LogWarning(error);
        }
    }
}