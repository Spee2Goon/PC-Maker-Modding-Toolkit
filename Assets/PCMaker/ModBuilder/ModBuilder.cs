#if UNITY_EDITOR

using System;
using System.IO;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace Mod.ModBuilder
{
    public enum ModBuildType
    {
        Debug = 0,
        Release = 1,
    }

    public static class ModBuilder
    {
        public const string ModExtension = "zip";
        public const string PathToModInfo = "Assets/Mod/ModInfo.asset";
        public const string PathToAssemblyDefinition = "Assets/Mod/ModAssemblyDefinition.asmdef";


        public static void BuildMod(string pathForSave, SO_ModInfo modInfo, ModBuildType targetBuildType)
        {
            ModBuildLoger.Clear();
            
            string pathToDLLsTempFolder = CreateTempFolder($"{Application.dataPath}/../", "DLLsTemp");

            try
            {
                string pathToTempFolder = CreateTempFolder(pathForSave, modInfo.ModName);


                AssemblyBuilder.BuildAssembly(pathToDLLsTempFolder, modInfo, targetBuildType);

                AddressablesBuilder.Build(pathToTempFolder);

                ManifestBuilder.Build(pathToTempFolder, modInfo);

                PackMod(pathToTempFolder, pathToDLLsTempFolder, pathForSave, modInfo);
            }
            catch (Exception ex)
            {
                Debug.LogError("Build Mod Failed");
                Debug.LogException(ex);
            }
            
            ModBuildLoger.Print();
            
            Directory.Delete(pathToDLLsTempFolder, true);
            
            ClearAfterBuild();
        }

        private static string CreateTempFolder(string pathForSave, string folderName)
        {
            string pathToTemporalFolder = Path.Combine(pathForSave, folderName);

            Directory.CreateDirectory(pathToTemporalFolder);

            return Path.GetFullPath(pathToTemporalFolder);
        }

        private static void PackMod(string pathToTemporalFolder, string pathToDLLsTempFolder, string pathToSave, SO_ModInfo modInfo)
        {
            string pathToZip = Path.Combine(pathToSave, modInfo.ModName + $".{ModExtension}");

            if (File.Exists(pathToZip))
            {
                for (int i = 1; i < 8388608; i++)
                {
                    pathToZip = Path.Combine(pathToSave, modInfo.ModName + $"_{i}" + $".{ModExtension}");

                    if (!File.Exists(pathToZip))
                    {
                        break;
                    }
                }
            }

            string mainDLLName = modInfo.ModName.Replace(" ", "");
            string pathToMainDLL = Path.Combine(pathToDLLsTempFolder, "Mod." + mainDLLName + ".dll");
            ModBuildLoger.Add($"get {pathToMainDLL}");
            
            if (File.Exists(pathToMainDLL))
            {
                File.Copy(pathToMainDLL, Path.Combine(pathToTemporalFolder, "Mod." + mainDLLName + ".dll"));
            }
            
            
            ZipFile.CreateFromDirectory(pathToTemporalFolder, pathToZip, System.IO.Compression.CompressionLevel.Optimal, includeBaseDirectory: false);
            
            Directory.Delete(pathToTemporalFolder, true);
        }

        private static void ClearAfterBuild()
        {
            Addressables.ClearResourceLocators();
            Caching.ClearCache();
        }
    }
}

#endif