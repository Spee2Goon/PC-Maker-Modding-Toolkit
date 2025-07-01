#if UNITY_EDITOR

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
        public const string ModExtension = "mod";
        public const string PathToModAssets = "Assets/Mod/";
        public const string PathToModInfo = "Assets/Mod/ModInfo.asset";
        public const string PathToModResources = "Assets/Mod/ModResources.asset";
        public const string PathToAssemblyDefinition = "Assets/Mod/ModAssemblyDefinition.asmdef";


        public static void BuildMod(string pathForSave, SO_ModInfo modInfo, ModBuildType targetBuildType)
        {
            string pathToTempFolder = CreateTempFolder(pathForSave, modInfo.ModName);

            AssemblyBuilder.BuildAssembly(PathToAssemblyDefinition, pathToTempFolder, targetBuildType);

            AddressablesBuilder.Build(pathToTempFolder, modInfo);

            ManifestBuilder.Build(pathToTempFolder, modInfo);

            PackMod(pathToTempFolder, pathForSave, modInfo);

            ClearAfterBuild();
        }

        private static string CreateTempFolder(string pathForSave, string folderName)
        {
            string pathToTemporalFolder = Path.Combine(pathForSave, folderName);

            Directory.CreateDirectory(pathToTemporalFolder);

            return pathToTemporalFolder;
        }

        private static void PackMod(string pathToTemporalFolder, string pathToSave, SO_ModInfo modInfo)
        {
            string pathToZip = Path.Combine(pathToSave, modInfo.ModName + $".{ModExtension}");

            if (File.Exists(pathToZip))
            {
                for (int i = 2; i < 16384; i++)
                {
                    pathToZip = Path.Combine(pathToSave, modInfo.ModName + $"_{i}" + $".{ModExtension}");

                    if (!File.Exists(pathToZip))
                    {
                        break;
                    }
                }
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