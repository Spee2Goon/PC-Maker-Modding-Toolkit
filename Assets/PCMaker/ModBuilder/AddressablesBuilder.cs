#if UNITY_EDITOR

using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;

namespace Mod.ModBuilder
{
    public static class AddressablesBuilder
    {
        public const string ModLoadVariable = "{PcMakerModsPath}";

        public static void Build(string pathForSave)
        {
            SetBuildPath(pathForSave);
            SetLoadPath(ModLoadVariable);

            AddressableAssetSettings.BuildPlayerContent(out AddressablesPlayerBuildResult result);

            if (string.IsNullOrEmpty(result.Error))
            {
                ModBuildLoger.Add("Mod Content Build Success!");
            }
            else
            {
                ModBuildLoger.Add("Build Error: " + result.Error);
            }
        }


        public static void SetBuildPath(string path)
        {
            AddressableAssetSettingsDefaultObject.Settings.profileSettings.CreateValue("ModBuildPath", path);
            AddressableAssetSettingsDefaultObject.Settings.profileSettings.SetValue(AddressableAssetSettingsDefaultObject.Settings.activeProfileId, "ModBuildPath", path);
        }

        public static void SetLoadPath(string path)
        {
            AddressableAssetSettingsDefaultObject.Settings.profileSettings.CreateValue("ModLoadPath", path);
            AddressableAssetSettingsDefaultObject.Settings.profileSettings.SetValue(AddressableAssetSettingsDefaultObject.Settings.activeProfileId, "ModLoadPath", path);
        }
    }
}

#endif