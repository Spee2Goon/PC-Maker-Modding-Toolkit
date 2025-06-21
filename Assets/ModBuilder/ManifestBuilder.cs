using System.IO;
using UnityEngine;

public static class ManifestBuilder
{
    public static void Build(string pathForSave, SO_ModInfo modInfo)
    {
        string path = Path.Combine(pathForSave, "manifest.json");

        string manifest =
            @$"{{
                ""ModName"": ""{modInfo.ModName}"",
                ""ModVersion"": ""{modInfo.ModVersion}""
            }}";

        File.WriteAllText(path, manifest);
    }
}
