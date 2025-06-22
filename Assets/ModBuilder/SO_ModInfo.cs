using UnityEngine;

namespace Mod.ModBuilder
{
    public class SO_ModInfo : ScriptableObject
    {
        public string ModName = DefaultModName;
        public string ModVersion = "1";


        public const string DefaultModName = "Your Mod Name";
    }
}