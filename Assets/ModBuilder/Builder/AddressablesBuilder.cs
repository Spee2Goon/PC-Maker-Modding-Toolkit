#if UNITY_EDITOR

using System.IO;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public static class AddressablesBuilder
{
    public static void Build(string pathForSave, SO_ModInfo modInfo)
    {
        AddressableAssetSettings.BuildPlayerContent(out AddressablesPlayerBuildResult result);

        if (string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Mod Content Build Success!");

            MoveBuildInfo(pathForSave);
            MoveBundles(pathForSave);
        }
        else
        {
            Debug.LogError("Build Error: " + result.Error);
        }
    }


    public static void MoveBuildInfo(string path)
    {
        string pathToAsset = Application.dataPath + "/../" + "Library/" + "com.unity.addressables/" + "aa/" + "Windows/";

        string[] assetNames = new string[] { "catalog.json", "catalog.hash", "settings.json" };

        for (int i = 0; i < assetNames.Length; i++)
        {
            if (File.Exists(pathToAsset + assetNames[i]))
            {
                Debug.Log("Move: " + assetNames[i]);

                File.Move(pathToAsset + "/" + assetNames[i], path + "/" + assetNames[i]);
            }
        }
    }

    private static void MoveBundles(string path)
    {
        string pathToAsset = Application.dataPath + "/../" + "ServerData/" + "StandaloneWindows64";

        string[] pathsToBundles = Directory.GetFiles(pathToAsset, "*.bundle");


        for (int i = 0; i < pathsToBundles.Length; i++)
        {
            Debug.Log("Move: " + pathsToBundles[i]);

            File.Move(pathsToBundles[i], path + "/" + Path.GetFileName(pathsToBundles[i]));
        }
    }
}

#endif