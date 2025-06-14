using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class ModBuilder
{
    public const string ModExtension = "mod";

    public static void BuildMod(SO_Mod mod)
    {
        string pathForSave = EditorUtility.SaveFilePanel("Select path to save mod", "Assets/", mod.Name, ModExtension);

        if (string.IsNullOrEmpty(pathForSave))
        {
            return;
        }

        Debug.Log("Save mod at " + pathForSave);

        CompileSolution();
        PackResources(mod, pathForSave);
    }

    private static void CompileSolution()
    {
        string pathToMs = MSBuildPathFinder.GetMsBuildPath();

        Debug.Log("pathToMs: " + pathToMs);

        //BuildSolution
        string[] pathsToSLNs = Directory.GetFiles($"{Application.dataPath}/../", "*.sln");

        if (pathsToSLNs.Length > 0)
        {
            RunCompiler(pathToMs, Path.GetFullPath(pathsToSLNs[0]));
        }
        else
        {
            Debug.Log("Build without scripts, you have no solution");
        }
    }

    private static void RunCompiler(string pathToMs, string pathToSLN)
    {
        string arguments = $"/C \"{pathToMs}\" \"{pathToSLN}\" /p:Configuration=Release";

        ProcessStartInfo processInfo = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = arguments,
            CreateNoWindow = false,
            UseShellExecute = true,
        };

        Process process = Process.Start(processInfo);

        process.WaitForExit();
    }


    private static void PackResources(SO_Mod mod, string pathForSave)
    {
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.GetSettings(true);
        if (settings == null)
        {
            return;
        }

        AddressableAssetSettings.BuildPlayerContent();

        string modFolder = CreateModFolder(mod, pathForSave);
    }

    private static string CreateModFolder(SO_Mod mod, string pathForSave)
    {
        string modFolderPath = pathForSave + mod.name;
        Directory.CreateDirectory(modFolderPath);

        return modFolderPath;
    }
}