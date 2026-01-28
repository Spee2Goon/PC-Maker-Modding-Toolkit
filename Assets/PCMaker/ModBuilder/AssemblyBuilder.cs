#if UNITY_EDITOR

using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

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
            ""references"": [""ModBuilder"", ""PCMaker.Services"", ""Unity.Addressables"", ""Unity.ResourceManager"", ""VContainer"", ""Unity.Cinemachine""],
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


        public static void BuildAssembly(string pathForSave, SO_ModInfo modInfo, ModBuildType targetBuildType)
        {
            string pathToMs = MSBuildPathFinder.GetMsBuildPath();

            ModBuildLoger.Add("pathToMs: " + pathToMs);

            string CSProjName = "Mod." + modInfo.ModName;
            
            //string[] pathsToCSProj = Directory.GetFiles($"{Application.dataPath}/../", $"{CSProjName}.csproj");
            string[] pathsToSLNs = Directory.GetFiles($"{Application.dataPath}/../", "*.sln");
            
            if (pathsToSLNs.Length > 0)
            {
                string fullPathToCSProj = Path.GetFullPath(pathsToSLNs[0]);
                
                RunCompiler(pathToMs, fullPathToCSProj, pathForSave, targetBuildType == ModBuildType.Debug);
            }
            else
            {
                ModBuildLoger.Add($"Build without scripts, you have no {CSProjName}.csproj file");
            }
        }

        private static void RunCompiler(string pathToMs, string pathToCSProj, string pathForSave, bool compileWithDebug)
        {
            ModBuildLoger.Add("RunCompiler \n" +
                              $"pathToMs: {pathToMs}\n" +
                              $"pathToCSProj: {pathToCSProj}\n" +
                              $"pathForSave: {pathForSave}");


            string arguments = $"\"{pathToCSProj}\" /p:OutDir=\"{pathForSave}\"";

            arguments += " /t:Build";
            arguments += " /p:CopyLocal=false";
            arguments += " /p:CopyLocalLockFileAssemblies=false";
            
            if (compileWithDebug)
            {
                arguments += " /p:DebugSymbols=true /p:DebugType=full /p:DebugInfo=full";
            }
            else
            {
                arguments += " /p:DebugSymbols=false /p:DebugType=none /p:DebugInfo=none";
            }
            
            ModBuildLoger.Add("MSBuild args: " + arguments);

            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                FileName = pathToMs,
                Arguments = arguments,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                UseShellExecute = false,
                CreateNoWindow = false,
            };

            Process process = Process.Start(processInfo);

            process.WaitForExit();
            process.Close();
        }
    }
}

#endif