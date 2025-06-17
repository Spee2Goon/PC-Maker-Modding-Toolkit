public enum ModBuildType
{
    Debug = 0,
    Release = 1,
}

public static class ModBuilder
{
    public const string ModExtension = "mod";
    public const string PathToModInfo = "Assets/ModInfo.asset";
    public const string PathToModResources = "Assets/ModResources.asset";
    public const string PathToAssemblyDefinition = "Assets/ModAssemblyDefinition.asmdef";


    public static void BuildMod(string pathForSave, SO_ModInfo modInfo, ModBuildType targetBuildType)
    {
        AssemblyBuilder.BuildAssembly(PathToAssemblyDefinition, pathForSave, targetBuildType);
    }
}