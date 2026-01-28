using System;
using UnityEngine;

namespace PCMaker.Services
{
    [Serializable]
    public class SaveFileInfo
    {
        public SaveFileInfo(string name, int playtime, bool overridePreviewImage)
        {
            Name = name;
            Version = Application.version;
            CreatedAt = DateTime.UtcNow.ToString("o");

            Playtime = playtime;
            OverridePreviewImage = overridePreviewImage;
        }
        
        public string Version;
        public string Name;
        public string CreatedAt;
        public int Playtime;
        
        public DateTime FileWriteTime; //When zip file was changed

        public bool OverridePreviewImage;
        
        public DateTime CreatedAtDateTime => DateTime.Parse(CreatedAt, null, System.Globalization.DateTimeStyles.RoundtripKind);
        
        [NonSerialized]
        public byte[] PreviewBytes;
        [NonSerialized]
        public Texture2D Preview;
        [NonSerialized]
        public float Money;
        [NonSerialized]
        public string ZipPath;
        [NonSerialized]
        public DateTime LoadSaveTime;
    }
}
