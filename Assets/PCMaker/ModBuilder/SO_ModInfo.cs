using UnityEngine;

namespace Mod.ModBuilder
{
    public class SO_ModInfo : ScriptableObject
    {
        public string ModName = DefaultModName;
        public string ModVersion = "1.0.0";
        public bool CanBeDisabled = true;

        public const string DefaultModName = "Your Mod Name";
    }
}