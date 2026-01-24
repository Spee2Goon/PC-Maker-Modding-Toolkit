#if UNITY_EDITOR

using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Mod.ModBuilder
{
    public static class AssemblyBuilder
    {
        public static void CreateAssemblyDefinition(SO_ModInfo modInfo, string pathToAssemblyDefinition)
        {
            string ModAssemblyDefinitionNamespace = "Mod." + new string(modInfo.ModName.Where(c => !char.IsWhiteSpace(c)).ToArray());

            File.WriteAllText(pathToAssemblyDefinition, GetModAssemblyDefinitionJson(ModAssemblyDefinitionNamespace));

            AssetDatabase.Refresh();
        }

        private static string GetModAssemblyDefinitionJson(string rootNamespace)
        {
            return @$"
            {{
            ""name"": ""{rootNamespace}"",
            ""rootNamespace"": ""{rootNamespace}"",
            ""references"": [""ModBuilder"", ""PCMaker.ModAPI"", ""Unity.Addressables"", ""Unity.ResourceManager""],
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


        public static void BuildAssembly(string pathForSave, ModBuildType targetBuildType)
        {
            string pathToMs = MSBuildPathFinder.GetMsBuildPath();

            Debug.Log("pathToMs: " + pathToMs);

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
            Debug.Log("RunCompiler \n" +
                $"pathToMs: {pathToMs}\n" +
                $"pathToSLN: {pathToSLN}\n" +
                $"pathForSave: {pathForSave}");


            string arguments = $"\"{pathToSLN}\" /p:OutputPath=\"{pathForSave}\" ";

            if (compileWithDebug)
            {
                arguments += "/p:Configuration=Debug  /p:DebugSymbols=true /p:DebugType=full /p:DebugInfo=full";
            }
            else
            {
                arguments += "/p:Configuration=Release /p:DebugSymbols=false /p:DebugType=none /p:DebugInfo=none";
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
            string[] paths = new string[] {
            Path.Combine(pathForSave, "ModBuilder.dll"),
            Path.Combine(pathForSave, "ModBuilder.pdb"),
            Path.Combine(pathForSave, "PCMaker.ModAPI.dll"),
            Path.Combine(pathForSave, "PCMaker.ModAPI.pdb"),
        };

            for (int i = 0; i < paths.Length; i++)
            {
                if (File.Exists(paths[i]))
                {
                    File.Delete(paths[i]);
                }
            }
        }
    }
}
#endif