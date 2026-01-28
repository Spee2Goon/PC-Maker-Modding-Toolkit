#if UNITY_EDITOR

using System.IO;
using UnityEngine;

namespace Mod.ModBuilder
{
    public static class ManifestBuilder
    {
        public static void Build(string pathForSave, SO_ModInfo modInfo)
        {
            string path = Path.Combine(pathForSave, "manifest.json");

            string manifest = JsonUtility.ToJson(modInfo, true);
            
            File.WriteAllText(path, manifest);
        }
    }
}

#endif