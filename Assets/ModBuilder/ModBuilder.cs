using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class ModBuilder
{
    public const string ModExtension = "mod";
    public const string AssemblyDefinitionName = "Mod Assembly Definition";

    public static void RequestToBuildMod(SO_Mod mod)
    {
        string pathForSave = Path.GetDirectoryName(EditorUtility.SaveFilePanel("Select path to save mod", "Assets/", mod.Name, ModExtension));

        if (string.IsNullOrEmpty(pathForSave))
        {
            Debug.LogError("Invalid mod path");
            return;
        }

        string pathToAsmdef = Path.Combine(Application.dataPath, AssemblyDefinitionName) + ".asmdef";

        if (!File.Exists(pathToAsmdef))
        {
            Debug.LogError("You have no assembly definition");
            return;
        }

        BuildMod(mod, pathForSave, pathToAsmdef);
    }

    private static void BuildMod(SO_Mod mod, string pathForSave, string pathToAsmdef)
    {
        Debug.Log("Save mod at " + pathForSave);

        //CreateTempModFolder
        string pathToModFolder = Path.Combine(pathForSave, mod.Name);
        Directory.CreateDirectory(pathToModFolder);

        CompileSolution();

        PackResources(mod, pathToModFolder);
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


    private static void PackResources(SO_Mod mod, string pathToMod)
    {

    }
}