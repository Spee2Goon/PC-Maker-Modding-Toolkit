using UnityEngine;

namespace PCMaker.ModAPI
{
    public static class ModBuildLoger
    {
        private static string BuildLog;
        
        public static void Add(string log) => BuildLog += log + "\n";
        
        public static void Print() => Debug.Log(BuildLog);
        
        public static void Clear() => BuildLog = "Build Log:\n";
    }
}