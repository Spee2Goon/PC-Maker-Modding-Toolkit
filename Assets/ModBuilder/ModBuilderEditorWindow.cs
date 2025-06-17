using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
public class ModBuilderEditorWindow : EditorWindow
{
    private SO_ModInfo currentModInfo;
    private SO_ModResources currentModResources;
    private Object currentAssemblyDefinition;

    private ModBuildType targetBuildType;


    //Window staf
    private ReorderableList _reorderableList;

    [MenuItem("PC Maker/Build Mod")]
    public static void OpenWindow()
    {
        ModBuilderEditorWindow window = GetWindow<ModBuilderEditorWindow>("Mod Builder");

        window.currentModInfo = File.Exists(ModBuilder.PathToModInfo) ? AssetDatabase.LoadAssetAtPath<SO_ModInfo>(ModBuilder.PathToModInfo) : null;
        window.currentModResources = File.Exists(ModBuilder.PathToModResources) ? AssetDatabase.LoadAssetAtPath<SO_ModResources>(ModBuilder.PathToModResources) : null;
        window.currentAssemblyDefinition = File.Exists(ModBuilder.PathToAssemblyDefinition) ? AssetDatabase.LoadAssetAtPath<Object>(ModBuilder.PathToAssemblyDefinition) : null;

        window.Show();
    }

    private void OnGUI()
    {
        DrawModInfo();

        GUILayout.Space(20);
        DrawResourcesInfo();

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
        currentModInfo = CreateInstance<SO_ModInfo>();

        AssetDatabase.CreateAsset(currentModInfo, ModBuilder.PathToModInfo);

        AssetDatabase.Refresh();
    }

    private void DrawResourcesInfo()
    {
        GUILayout.Label("Resources Info", EditorStyles.boldLabel);

        currentModResources = (SO_ModResources)EditorGUILayout.ObjectField("Mod Resources Data", currentModResources, typeof(SO_ModResources), false);

        if (currentModResources == null)
        {
            if (GUILayout.Button("Create Mod Resources"))
            {
                CreateResourcesAsset();
            }
        }
        else
        {
            GUILayout.Space(5);

            List<TextAsset> scripts = currentModResources.Scripts;

            if (_reorderableList == null)
            {
                _reorderableList = new ReorderableList(currentModResources.Scripts, typeof(TextAsset), true, true, true, true);

                _reorderableList.drawHeaderCallback = (Rect rect) =>
                {
                    EditorGUI.LabelField(rect, "Scripts");
                };

                _reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    TextAsset script = currentModResources.Scripts[index];

                    script = (TextAsset)EditorGUI.ObjectField(rect, script, typeof(TextAsset), false);
                    
                    currentModResources.Scripts[index] = script;
                };
            }

            Rect rect = GUILayoutUtility.GetRect(1, _reorderableList.GetHeight());

            rect.x += 4;
            rect.width -= 8;

            _reorderableList.DoList(rect);
        }
    }

    private void CreateResourcesAsset()
    {
        currentModResources = CreateInstance<SO_ModResources>();

        AssetDatabase.CreateAsset(currentModResources, ModBuilder.PathToModResources);

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
            if (GUILayout.Button("Create Assembly Definition"))
            {
                CreateAssemblyDefinitionAsset();
            }
        }
    }

    private void CreateAssemblyDefinitionAsset()
    {
        AssemblyBuilder.CreateAssemblyDefinition(currentModInfo, ModBuilder.PathToAssemblyDefinition);

        currentAssemblyDefinition = AssetDatabase.LoadAssetAtPath<Object>(ModBuilder.PathToAssemblyDefinition);
    }

    private void DrawButtons()
    {
        GUI.enabled = currentAssemblyDefinition != null;

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