#if UNITY_EDITOR

using System.IO;
using UnityEditor;
using UnityEngine;

namespace Mod.ModBuilder
{
    public class ModBuilderEditorWindow : EditorWindow
    {
        private SO_ModInfo currentModInfo;
        private Object currentAssemblyDefinition;

        private ModBuildType targetBuildType;


        [MenuItem("PC Maker/Build Mod")]
        public static void OpenWindow()
        {
            ModBuilderEditorWindow window = GetWindow<ModBuilderEditorWindow>("Mod Builder");

            window.currentModInfo = File.Exists(ModBuilder.PathToModInfo) ? AssetDatabase.LoadAssetAtPath<SO_ModInfo>(ModBuilder.PathToModInfo) : null;
            window.currentAssemblyDefinition = File.Exists(ModBuilder.PathToAssemblyDefinition) ? AssetDatabase.LoadAssetAtPath<Object>(ModBuilder.PathToAssemblyDefinition) : null;

            window.Show();
        }

        private void OnGUI()
        {
            DrawModInfo();

            GUILayout.Space(20);
            DrawAssemblyInfo();

            GUILayout.Space(20);
            DrawButtons();
        }

        private void DrawModInfo()
        {
            GUILayout.Label("Mod Info", EditorStyles.boldLabel);

            currentModInfo = (SO_ModInfo)EditorGUILayout.ObjectField("Mod Info Data", currentModInfo, typeof(SO_ModInfo), false);

            if (currentModInfo == null)
            {
                if (GUILayout.Button("Create Mod Info"))
                {
                    CreateModInfoAsset();
                }
            }
            else
            {
                GUILayout.Space(5);

                currentModInfo.ModName = EditorGUILayout.TextField("Name", currentModInfo.ModName);

                currentModInfo.ModVersion = EditorGUILayout.TextField("Version", currentModInfo.ModVersion);
            }
        }

        private void CreateModInfoAsset()
        {
            if (!Directory.Exists(Path.GetDirectoryName(ModBuilder.PathToModInfo)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(ModBuilder.PathToModInfo));
            }

            currentModInfo = CreateInstance<SO_ModInfo>();

            AssetDatabase.CreateAsset(currentModInfo, ModBuilder.PathToModInfo);

            AssetDatabase.Refresh();
        }

        private void DrawAssemblyInfo()
        {
            GUILayout.Label("Assembly Info", EditorStyles.boldLabel);

            currentAssemblyDefinition = (Object)EditorGUILayout.ObjectField("Assembly Definition Asset", currentAssemblyDefinition, typeof(Object), false);


            if (currentAssemblyDefinition != null)
            {
                targetBuildType = (ModBuildType)EditorGUILayout.EnumPopup("Build Type", (ModBuildType)targetBuildType);
            }
            else
            {
                GUI.enabled = currentModInfo != null && currentModInfo.ModName != SO_ModInfo.DefaultModName;

                if (GUILayout.Button("Create Assembly Definition"))
                {
                    CreateAssemblyDefinitionAsset();
                }

                GUI.enabled = true;
            }
        }

        private void CreateAssemblyDefinitionAsset()
        {
            AssemblyBuilder.CreateAssemblyDefinition(currentModInfo, ModBuilder.PathToAssemblyDefinition);

            currentAssemblyDefinition = AssetDatabase.LoadAssetAtPath<Object>(ModBuilder.PathToAssemblyDefinition);
        }

        private void DrawButtons()
        {
            GUI.enabled = currentModInfo != null;

            if (GUILayout.Button("Build Mod"))
            {
                BuildMod();
            }
        }


        private void BuildMod()
        {
            //Path where save mod
            string pathForSave = EditorUtility.SaveFolderPanel("Select path to save mod", "Assets/", "");

            if (pathForSave == null || pathForSave == string.Empty)
            {
                return;
            }

            Debug.Log("Save mod at: " + pathForSave);

            ModBuilder.BuildMod(pathForSave, currentModInfo, targetBuildType);
        }
    }
}
#endif