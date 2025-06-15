using UnityEngine;

[CreateAssetMenu(fileName = "New Mod", menuName = "ModsObjects/New Mod")]
public class SO_Mod : ScriptableObject
{
    public string Name;
    public string Version = "1.0";



    [ContextMenu("Build To File")]
    public void Build()
    {
        ModBuilder.RequestToBuildMod(this);
    }
}
