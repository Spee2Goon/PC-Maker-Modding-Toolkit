using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class AssemblyBuilder
{
    public static void CreateAssemblyDefinition(SO_ModInfo modInfo, string pathToAssemblyDefinition)
    {
        string AssemblyDefinitionNamespace = new string(modInfo.ModName.Where(c => !char.IsWhiteSpace(c)).ToArray());

        File.WriteAllText(pathToAssemblyDefinition, GetAssemblyDefinitionJson(AssemblyDefinitionNamespace));

        AssetDatabase.Refresh();
    }

    private static string GetAssemblyDefinitionJson(string rootNamespace)
    {
        return @$"
            {{
            ""name"": ""{rootNamespace}"",
            ""rootNamespace"": ""{rootNamespace}"",
            ""references"": [""ModBuilder""],
            ""includePlatforms"": [],
            ""excludePlatforms"": [],
            ""allowUnsafeCode"": false,
            ""overrideReferences"": true,
            ""precompiledReferences"": [ ""Mono.Cecil.dll"" ],
            ""autoReferenced"": true,
            ""defineConstraints"": [],
            ""versionDefines"": [],
            ""noEngineReferences"": false
            }}";
    }


    public static void BuildAssembly(string pathToAssemblyDefinition, string pathForSave, ModBuildType targetBuildType)
    {
        string pathToMs = MSBuildPathFinder.GetMsBuildPath();

        Debug.Log("pathToMs: " + pathToMs);

        //BuildSolution
        string[] pathsToSLNs = Directory.GetFiles($"{Application.dataPath}/../", "*.sln");

        if (pathsToSLNs.Length > 0)
        {
            RunCompiler(pathToMs, Path.GetFullPath(pathsToSLNs[0]), pathForSave, targetBuildType == ModBuildType.Debug);
        }
        else
        {
            Debug.Log("Build without scripts, you have no solution");
        }
    }

    private static void RunCompiler(string pathToMs, string pathToSLN, string pathForSave, bool compileWithDebug)
    {
        Debug.Log($"RunCompiler \n" +
            $"pathToMs: {pathToMs}\n" +
            $"pathToSLN: {pathToSLN}\n" +
            $"pathForSave: {pathForSave}");


        string arguments = $"\"{pathToSLN}\" /p:OutputPath=\"{pathForSave}\" ";

        if (compileWithDebug)
        {
            arguments += $"/p:Configuration=Debug  /p:DebugSymbols=true /p:DebugType=full /p:DebugInfo=full";
        }
        else
        {
            arguments += $"/p:Configuration=Release /p:DebugSymbols=false /p:DebugType=none /p:DebugInfo=none";
        }


        ProcessStartInfo processInfo = new ProcessStartInfo
        {
            FileName = pathToMs,
            Arguments = arguments,
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        Process process = Process.Start(processInfo);

        process.WaitForExit();
        process.Close();

        ClearBuilderCompileInfo(pathForSave);
    }

    private static void ClearBuilderCompileInfo(string pathForSave)
    {
        string pathToBuilderDll = Path.Combine(pathForSave, "ModBuilder.dll");

        if (File.Exists(pathToBuilderDll))
        {
            File.Delete(pathToBuilderDll);
        }

        string pathToBuilderPDB = Path.Combine(pathForSave, "ModBuilder.pdb");

        if (File.Exists(pathToBuilderPDB))
        {
            File.Delete(pathToBuilderPDB);
        }
    }
}